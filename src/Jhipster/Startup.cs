using System;
using Jhipster.Infrastructure.Data;
using Jhipster.Configuration;
using Jhipster.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Jhipster.Infrastructure.Shared;
using Serilog;
using MediatR;
using System.Reflection;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Jhipster.Domain.Repositories.Interfaces;
using Jhipster.Infrastructure.Data.Repositories;
using Module.Redis;
using Microsoft.AspNetCore.Http.Features;
using InteractiveSvc;
using BFF.Web;
using OrderSvc;

[assembly: ApiController]

namespace Jhipster
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10485760; // Dung lượng tối đa cho mỗi yêu cầu là 10MB (10 * 1024 * 1024 bytes)
            });
            services
                .AddAppSettingsModule(Configuration);


            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            AddDatabase(services);

            AddModule(services);

            AddBffGateway(services);

            //AddSerilog(Configuration);

            services
                .AddSecurityModule()
                .AddProblemDetailsModule(Environment)
                .AddAutoMapperModule()
                .AddSwaggerModule()
                .AddWebModule()
                .AddRepositoryModule()
                .AddServiceModule();

            services
                .AddMailModule(Configuration);
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            //    });
            //});
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("*").WithMethods("*").WithHeaders("*").WithExposedHeaders("*");
            }));

 /*           services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificUrl",
                    builder => builder.WithOrigins("*")
                    .WithOrigins("https://api.pharmadi.com.vn")
                    .WithOrigins("https://adm.pharmadi.com.vn")
                    .WithOrigins("https://pharmadi.vn")
                    .WithOrigins("https://api.pharmadi.vn")
                    .WithOrigins("https://adm.pharmadi.vn"));
            });*/

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider,
            ApplicationDatabaseContext context, IOptions<SecuritySettings> securitySettingsOptions)
        {
            var securitySettings = securitySettingsOptions.Value;
            app.UseSerilogRequestLogging();
            app
                .UseApplicationSecurity(securitySettings)
                .UseApplicationProblemDetails()
                .UseApplicationSwagger()
                .UseApplicationWeb(env)
                .UseApplicationDatabase(serviceProvider, env)
                .UseApplicationIdentity(serviceProvider);
            app
                .UseCors();


        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            services.AddDatabaseModule(Configuration);
        }

        protected virtual void AddModule(IServiceCollection services)
        {
            // General Infras
            services.AddSharedInfrastructure(Configuration);
            services.AddScoped(typeof(IJwtRepository), typeof(JwtRepository));
            // Module
           
            services.AddInteractiveModule(Configuration);
            services.AddOrderModule(Configuration);
            //// Redis
            services.AddRedisModule(Configuration);

        }



        protected virtual void AddBffGateway(IServiceCollection services)
        {
            //services.AddBFFWebModule(Configuration);
            //services.AddCodeFirstGrpcReflection();
            services.AddBFFWebModule(Configuration);
        }
    }
}
