FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/BroadcastBoard.Domain/*.csproj ./src/BroadcastBoard.Domain/
COPY src/BroadcastBoard.Application/*.csproj ./src/BroadcastBoard.Application/
COPY src/BroadcastBoard.Infrastructure/*.csproj ./src/BroadcastBoard.Infrastructure/
COPY src/BroadcastBoard.Api/*.csproj ./src/BroadcastBoard.Api/
COPY tests/BroadcastBoard.Tests/*.csproj ./tests/BroadcastBoard.Tests/
RUN dotnet restore

COPY . .
WORKDIR /app/src/BroadcastBoard.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/BroadcastBoard.Api/out ./
ENTRYPOINT ["dotnet", "BroadcastBoard.Api.dll"]
EXPOSE 5000
