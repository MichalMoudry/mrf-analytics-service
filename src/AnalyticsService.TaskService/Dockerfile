#FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["AnalyticsService.TaskService/AnalyticsService.TaskService.csproj", "AnalyticsService.TaskService/"]
#RUN dotnet restore "AnalyticsService.TaskService/AnalyticsService.TaskService.csproj"
#COPY . .
#WORKDIR "/src/AnalyticsService.TaskService"
#RUN dotnet build "AnalyticsService.TaskService.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "AnalyticsService.TaskService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AnalyticsService.TaskService.dll"]

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
