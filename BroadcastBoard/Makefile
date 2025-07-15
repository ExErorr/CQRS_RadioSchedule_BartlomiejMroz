build:
	dotnet build

test:
	dotnet test

run:
	dotnet run --project src/BroadcastBoard.Api

docker-build:
	docker build -t broadcastboard -f Dockerfile .

docker-run:
	docker run -p 5000:5000 broadcastboard
