FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG TARGET_PLATFORM=x64
WORKDIR /app
RUN apt update
RUN apt install -y clang zlib1g-dev

COPY ["AnalyticsService.TaskService/AnalyticsService.TaskService.csproj", "AnalyticsService.TaskService/"]
COPY ["AnalyticsService.Database/AnalyticsService.Database.csproj", "AnalyticsService.Database/"]
# Restore as distinct layers
RUN dotnet restore ./AnalyticsService.TaskService/AnalyticsService.TaskService.csproj
# Build and publish a release
RUN dotnet publish ./AnalyticsService.TaskService -c Release -a $TARGET_PLATFORM -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy-chiseled AS release
WORKDIR /app
# Copy persistance layer library artifacts
COPY --from=build-env /app/out/AnalyticsService.Database* .
# Copy settings
COPY --from=build-env /app/out/appsettings* .
# Copy task service
COPY --from=build-env /app/out/AnalyticsService.TaskService .

ENTRYPOINT [ "/app/AnalyticsService.TaskService" ]