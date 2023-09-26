create_local_db:
	docker run -d --name analytics-serv-db -p 5432:5432 -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -e POSTGRES_DB=analytics-serv-db postgres:15

build_image:
	docker build . -t analytics-service:v1.0.0
