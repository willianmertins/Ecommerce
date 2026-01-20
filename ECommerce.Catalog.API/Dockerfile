FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.Catalog.API/ECommerce.Catalog.API.csproj", "ECommerce.Catalog.API/"]
COPY ["ECommerce.Common/ECommerce.Common.csproj", "ECommerce.Common/"]
COPY ["ECommerce.EventBus/ECommerce.EventBus.csproj", "ECommerce.EventBus/"]
COPY ["ECommerce.Logging/ECommerce.Logging.csproj", "ECommerce.Logging/"]
RUN dotnet restore "ECommerce.Catalog.API/ECommerce.Catalog.API.csproj"
COPY . .
WORKDIR "/src/ECommerce.Catalog.API"
RUN dotnet build "./ECommerce.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Catalog.API.dll"]
