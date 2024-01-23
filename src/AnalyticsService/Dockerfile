# Possible images to use:
# /aspnet:8.0-jammy-chiseled
# /aspnet:8.0-alpine (this might be broken atm)
# alpine:3.19 (for self-contained deployment)
FROM alpine:3.19 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#ARG TARGET_PLATFORM=linux-x64
#WORKDIR /src
#COPY ["AnalyticsService/AnalyticsService.fsproj", "AnalyticsService/"]
#RUN dotnet restore "AnalyticsService/AnalyticsService.fsproj"
#COPY . .
#WORKDIR "/src/AnalyticsService"
#RUN dotnet build "AnalyticsService.fsproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#ARG TARGET_PLATFORM=linux-x64
#RUN dotnet publish "AnalyticsService.fsproj" -c $BUILD_CONFIGURATION -r $TARGET_PLATFORM -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AnalyticsService.dll"]

# -- optimized version --
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGET_PLATFORM=linux-x64
WORKDIR /src
COPY ["AnalyticsService/AnalyticsService.fsproj", "AnalyticsService/"]
RUN dotnet restore "AnalyticsService/AnalyticsService.fsproj"
COPY . .
WORKDIR "/src/AnalyticsService"
RUN dotnet build "AnalyticsService.fsproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGET_PLATFORM=linux-x64
RUN dotnet publish -r $TARGET_PLATFORM -o /app/publish --self-contained

FROM base AS release
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AnalyticsService.dll"]
ENTRYPOINT [ "AnalyticsService" ]
