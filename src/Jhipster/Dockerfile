#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Jhipster/Jhipster.csproj", "src/Jhipster/"]
COPY ["src/Jhipster.BFF/BFF.Web/BFF.Web.csproj", "src/Jhipster.BFF/BFF.Web/"]
COPY ["src/Jhipster.Domain.Services/Jhipster.Domain.Services.csproj", "src/Jhipster.Domain.Services/"]
COPY ["src/Jhipster.gRPC.Contracts.Shared/Jhipster.gRPC.Contracts.Shared.csproj", "src/Jhipster.gRPC.Contracts.Shared/"]
COPY ["src/Jhipster.Crosscutting/Jhipster.Crosscutting.csproj", "src/Jhipster.Crosscutting/"]
COPY ["src/Jhipster.Domain/Jhipster.Domain.csproj", "src/Jhipster.Domain/"]
COPY ["src/Jhipster.Dto/Jhipster.Dto.csproj", "src/Jhipster.Dto/"]
COPY ["src/Jhipster.Infrastructure/Jhipster.Infrastructure.csproj", "src/Jhipster.Infrastructure/"]
COPY ["src/Jhipster.Module/OrderSvc/OrderSvc.Domain/OrderSvc.Domain.csproj", "src/Jhipster.Module/OrderSvc/OrderSvc.Domain/"]
COPY ["src/Jhipster.Module/OrderSvc/OrderSvc.Application/OrderSvc.Application.csproj", "src/Jhipster.Module/OrderSvc/OrderSvc.Application/"]
COPY ["src/Jhipster.Module/OrderSvc/OrderSvc.Share/OrderSvc.Share.csproj", "src/Jhipster.Module/OrderSvc/OrderSvc.Share/"]
COPY ["src/Jhipster.Module/Redis/Module.Redis.Library/Module.Redis.Library.csproj", "src/Jhipster.Module/Redis/Module.Redis.Library/"]
COPY ["src/Jhipster.Module/Redis/Module.Redis/Module.Redis.csproj", "src/Jhipster.Module/Redis/Module.Redis/"]
COPY ["src/Jhipster.Module/OrderSvc/OrderSvc/OrderSvc.csproj", "src/Jhipster.Module/OrderSvc/OrderSvc/"]
COPY ["src/Jhipster.Module/OrderSvc/OrderSvc.Infrastructure/OrderSvc.Infrastructure.csproj", "src/Jhipster.Module/OrderSvc/OrderSvc.Infrastructure/"]
RUN dotnet restore "src/Jhipster/Jhipster.csproj"
COPY . .
WORKDIR "/src/src/Jhipster"
RUN dotnet build "Jhipster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jhipster.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jhipster.dll"]