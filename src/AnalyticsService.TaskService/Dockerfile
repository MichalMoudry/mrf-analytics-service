FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
RUN apt update
RUN apt install -y clang zlib1g-dev

COPY ./AnalyticsService.TaskService ./AnalyticsService.TaskService
COPY ./AnalyticsService.Database ./AnalyticsService.Database
# Restore as distinct layers
RUN dotnet restore ./AnalyticsService.TaskService/AnalyticsService.TaskService.fsproj
# Build and publish a release
RUN dotnet publish ./AnalyticsService.TaskService -c Release -a x86 -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS release
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT [ "/app/AnalyticsService.TaskService" ]
#ENTRYPOINT ["tail", "-f", "/dev/null"]
