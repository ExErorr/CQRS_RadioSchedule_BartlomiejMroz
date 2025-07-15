## Uruchomienie lokalnie

Wpisujemy z root projektu
make build  
make run

## Uruchomienie w Dockerze

Wpisujemy z root projektu
make docker-build  
make docker-run

## Swagger

http://localhost:5000/swagger

## Dodanie audycji

curl -X POST http://localhost:5000/api/shows -H "accept: _/_" -H "Content-Type: application/json" -d "{\"title\":\"Rozmowa z Panem Janem\",\"presenter\":\"Jan\",\"startTime\":\"2025-07-15T19:06:37.523Z\",\"durationMinutes\":30}"

## Kolizja do powyższej audycji

curl -X POST http://localhost:5000/api/shows -H "accept: _/_" -H "Content-Type: application/json" -d "{\"title\":\"Rozmowa z Panem Michalem\",\"presenter\":\"Michal\",\"startTime\":\"2025-07-15T19:26:37.523Z\",\"durationMinutes\":30}"

## Pobranie audycji po ID

curl -X GET http://localhost:5000/api/shows/83580e56-8ac7-48b4-9471-67055f24a90c -H "accept: _/_"

## Ramówka dnia

curl -X GET "http://localhost:5000/api/shows?date=2025-07-15" -H "accept: _/_"

## Struktura katalogów

- `src/BroadcastBoard.Api` – WebAPI
- `src/BroadcastBoard.Application` – logika aplikacyjna (CQRS, DTO, Exceptions)
- `src/BroadcastBoard.Domain` – encje domenowe i interfejsy
- `src/BroadcastBoard.Infrastructure` – logowanie, repozytoria itp.
- `test/BroadcastBoard.Tests` – testy jednostkowe
