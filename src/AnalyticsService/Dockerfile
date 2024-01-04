#FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGET_PLATFORM=x64
WORKDIR /app
#RUN apt update
#RUN apt install -y clang zlib1g-dev

COPY ./AnalyticsService ./AnalyticsService
COPY ./AnalyticsService.Database ./AnalyticsService.Database
# Restore as distinct layers
RUN dotnet restore ./AnalyticsService/AnalyticsService.fsproj
# Build and publish a release
RUN dotnet publish ./AnalyticsService -c Release -a $TARGET_PLATFORM -o out /p:UseAppHost=true

# Build runtime image
# Possible images to use:
# :8.0-jammy-chiseled
# :8.0-alpine (this might be broken atm)
FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy-chiseled AS release
USER $APP_UID
WORKDIR /app
# Copy persistance layer library artifacts
COPY --from=build /app/out/AnalyticsService.Database* .
# Copy settings
COPY --from=build /app/out/appsettings* .
# Copy web service
COPY --from=build /app/out/AnalyticsService .

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT [ "/app/AnalyticsService" ]
#ENTRYPOINT ["tail", "-f", "/dev/null"]
