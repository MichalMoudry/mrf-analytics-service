FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AnalyticsService.TaskService/AnalyticsService.TaskService.fsproj", "AnalyticsService.TaskService/"]
RUN dotnet restore "AnalyticsService.TaskService/AnalyticsService.TaskService.fsproj"
COPY . .
WORKDIR "/src/AnalyticsService.TaskService"
RUN dotnet build "AnalyticsService.TaskService.fsproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AnalyticsService.TaskService.fsproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsService.TaskService.dll"]
