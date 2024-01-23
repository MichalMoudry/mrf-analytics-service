#!/bin/bash
dapr run --app-id analytics-service --app-port 5210 --resources-path ../.dapr -- dotnet run --project ./AnalyticsService