FROM mcr.microsoft.com/dotnet/aspnet:7.0.14-alpine3.18 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AnalyticsService.fsproj ./
COPY . .
RUN dotnet build AnalyticsService.fsproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnalyticsService.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsService.dll"]
