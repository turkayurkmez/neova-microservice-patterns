# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Neova.Catalog.API/Neova.Catalog.API.csproj", "src/Services/Catalog/Neova.Catalog.API/"]
COPY ["src/Services/Catalog/Neova.Catalog.Infrastructure/Neova.Catalog.Infrastructure.csproj", "src/Services/Catalog/Neova.Catalog.Infrastructure/"]
COPY ["src/Shared/Neova.Shared.EventBus/Neova.Shared.EventBus.csproj", "src/Shared/Neova.Shared.EventBus/"]
COPY ["src/Services/Catalog/Neova.Catalog.Application/Neova.Catalog.Application.csproj", "src/Services/Catalog/Neova.Catalog.Application/"]
COPY ["src/Services/Catalog/Neova.Catalog.Domain/Neova.Catalog.Domain.csproj", "src/Services/Catalog/Neova.Catalog.Domain/"]
COPY ["src/Shared/Neova.Shared.Library/Neova.Shared.Library.csproj", "src/Shared/Neova.Shared.Library/"]
RUN dotnet restore "./src/Services/Catalog/Neova.Catalog.API/Neova.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Neova.Catalog.API"
RUN dotnet build "./Neova.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Neova.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neova.Catalog.API.dll"]