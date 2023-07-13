using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jhipster.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Jhipster.Configuration
{
    public static class IdentityConfiguration
    {
        public static IApplicationBuilder UseApplicationIdentity(this IApplicationBuilder builder,
            IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                SeedRoles(roleManager).Wait();
                SeedUsers(userManager).Wait();
                SeedUserRoles(userManager).Wait();
            }

            return builder;
        }


        private static IEnumerable<Role> Roles()
        {
            return new List<Role> {
                new Role {Id = "role_supervior", Name = "ROLE_SUPERVIOR"},
                new Role {Id = "role_admin",Name = "ROLE_ADMIN"},
                new Role {Id = "role_anonymous", Name = "ROLE_ANONYMOUS"},
                new Role {Id = "role_merchant",Name = "ROLE_MERCHANT"},
            };
        }

        private static IEnumerable<User> Users()
        {
            return new List<User> {
                new User {
                    Id = "user-0",
                    UserName = "supervior",
                    PasswordHash = "$2a$10$mE.qmcV0mFU5NcKh73TZx.z4ueI/.bDWbj0T1BYyqP481kGGarKLG",
                    FirstName = "supervior",
                    LastName = "supervior",
                    Email = "supervior@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "user-1",
                    UserName = "admin",
                    PasswordHash = "$2a$10$gSAhZrxMllrbgj/kkK9UceBPpChGWJA7SYIb1Mqo.n5aNLq1/oRrC",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "user-2",
                    UserName = "merchant",
                    PasswordHash = "$2a$10$VEjxo0jq2YG9Rbk2HmX9S.k1uZBGYUHdUcid3g/vfiEl7lwWgOH/K",
                    FirstName = "merchant",
                    LastName = "merchant",
                    Email = "merchant@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "user-3",
                    UserName = "anonymoususer",
                    PasswordHash = "$2a$10$j8S5d7Sr7.8VTOYNviDPOeWX8KcYILUVJBsYV83Y5NtECayypx9lO",
                    FirstName = "Anonymous",
                    LastName = "Anonymous",
                    Email = "anonymous@localhost",
                    Activated = true,
                    LangKey = "en"
                }
            };
        }

        private static IDictionary<string, string[]> UserRoles()
        {
            return new Dictionary<string, string[]> {
                {"user-0", new[] { "ROLE_SUPERVIOR", "ROLE_ADMIN", "ROLE_MERCHANT","ROLE_ANONYMOUS"}},
                {"user-1", new[] {"ROLE_ADMIN", "ROLE_MERCHANT","ROLE_ANONYMOUS"}},
                {"user-2", new[] {"ROLE_MERCHANT","ROLE_ANONYMOUS"}},
                {"user-3", new[] {"ROLE_ANONYMOUS"}}
            };
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles())
            {
                var dbRole = await roleManager.FindByNameAsync(role.Name);
                if (dbRole == null)
                {
                    await roleManager.CreateAsync(role);
                }
                else
                {
                    await roleManager.UpdateAsync(dbRole);
                }
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager)
        {
            foreach (var user in Users())
            {
                var dbUser = await userManager.FindByIdAsync(user.Id);
                if (dbUser == null)
                {
                    await userManager.CreateAsync(user);
                }
                else
                {
                    await userManager.UpdateAsync(dbUser);
                }
            }
        }

        private static async Task SeedUserRoles(UserManager<User> userManager)
        {
            foreach (var (id, roles) in UserRoles())
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}


