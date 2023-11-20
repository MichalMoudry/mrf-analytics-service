create_local_db:
	docker run -d --name data-persistence -p 5432:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -e POSTGRES_DB=data-persistence postgres:15

migrate_local_db:
	migrate -path './AnalyticsService/Database/Migrations' -database 'postgres://root:root@localhost:5432/data-persistence?sslmode=disable&search_path=analytics' up

downgrade_local_db:
	migrate -path './AnalyticsService/Database/Migrations' -database 'postgres://root:root@localhost:5432/data-persistence?sslmode=disable&search_path=analytics' down 1

build_image:
	docker build . -t analytics-service:v1.0.0

run_with_dapr:
	dapr run --app-id analyticsservice --app-port 5116 --dapr-http-port 3601 --dapr-grpc-port 60001 --resources-path ../.dapr -- dotnet run --project ./AnalyticsService/AnalyticsService.fsproj
