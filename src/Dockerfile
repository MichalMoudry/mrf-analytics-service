FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Restore project and solution
COPY *.sln .
COPY AnalyticsService/*.fsproj ./AnalyticsService/
COPY AnalyticsService.Test/*.fsproj ./AnalyticsService.Test/
COPY AnalyticsService.BgRunner/*.fsproj ./AnalyticsService.BgRunner/
RUN dotnet restore

# Copy source code and build the app
COPY AnalyticsService/. ./AnalyticsService/
WORKDIR /source/AnalyticsService
RUN dotnet publish -c release -o /app
# --no-restore

# Create release image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENTRYPOINT [ "dotnet", "AnalyticsService.dll" ]