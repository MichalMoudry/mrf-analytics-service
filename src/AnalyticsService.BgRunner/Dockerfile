FROM mcr.microsoft.com/dotnet/runtime:7.0.14-alpine3.18 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY AnalyticsService.BgRunner.fsproj ./
COPY . .
RUN dotnet build AnalyticsService.BgRunner.fsproj -c Release -o /app/build
#COPY ["AnalyticsService.BgRunner/AnalyticsService.BgRunner.fsproj", "AnalyticsService.BgRunner/"]
#RUN dotnet restore "AnalyticsService.BgRunner/AnalyticsService.BgRunner.fsproj"
#COPY . .
#WORKDIR "/src/AnalyticsService.BgRunner"
#RUN dotnet build "AnalyticsService.BgRunner.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnalyticsService.BgRunner.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsService.BgRunner.dll"]
