create_local_db:
	docker run -d --name analytics-serv-db -p 5432:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -e POSTGRES_DB=analytics-serv-db postgres:15

migrate_local_db:
	migrate -path './AnalyticsService/Database/Migrations' -database 'postgres://root:root@localhost:5432/analytics-serv-db?sslmode=disable' up

build_image:
	docker build . -t analytics-service:v1.0.0
