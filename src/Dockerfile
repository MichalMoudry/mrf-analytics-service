FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-alpine3.18 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AnalyticsService.Database/. ./AnalyticsService.Database
COPY AnalyticsService/. ./AnalyticsService
RUN dotnet build AnalyticsService/AnalyticsService.fsproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnalyticsService/AnalyticsService.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsService.dll"]
