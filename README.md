# Room Reservation API

Aplikacja ASP.NET Core Web API do zarządzania salami szkoleniowymi i ich rezerwacjami.

## Przykładowe żądania

### Dodaj salę

```http
POST /api/rooms
Content-Type: application/json

{
  "name": "Lab 204",
  "buildingCode": "B2",
  "floor": 2,
  "capacity": 24,
  "hasProjector": true,
  "isActive": true
}
```

### Dodaj rezerwację

```http
POST /api/reservation
Content-Type: application/json

{
  "roomId": 2,
  "organizerName": "Anna Kowalska",
  "topic": "Warsztaty z HTTP i REST",
  "startTime": "2026-05-10T10:00:00",
  "endTime": "2026-05-10T12:30:00",
  "status": "Confirmed"
}
```

## Kody odpowiedzi HTTP

| Kod | Znaczenie |
|-----|-----------|
| 200 OK | Poprawny odczyt |
| 201 Created | Zasób utworzony |
| 204 No Content | Poprawna aktualizacja lub usunięcie |
| 400 Bad Request | Błędne dane wejściowe |
| 404 Not Found | Zasób nie istnieje |
| 409 Conflict | Konflikt czasowy rezerwacji lub sala ma przyszłe rezerwacje |

## Reguły biznesowe

- Nie można zarezerwować sali, która nie istnieje
- Nie można zarezerwować sali oznaczonej jako nieaktywna (`isActive: false`)
- Dwie rezerwacje tej samej sali nie mogą nakładać się czasowo tego samego dnia
- Nie można usunąć sali, dla której istnieją przyszłe rezerwacje

## Walidacja danych wejściowych

- `Name` - wymagane, maks. 15 znaków
- `BuildingCode` - wymagane, maks. 10 znaków
- `Capacity` - wymagane, minimum 1
- `OrganizerName` - wymagane
- `Topic` - wymagane
- `EndTime` musi być późniejsze niż `StartTime`

## Dane startowe

Aplikacja startuje z 5 salami i 6 przykładowymi rezerwacjami zdefiniowanymi w klasie `DataStore`.

---

## Architektura

Projekt podzielony jest na trzy warstwy. Warstwa modeli (`Models/`) zawiera klasy `Room` i `Reservation` reprezentujące dane domenowe. Warstwa DTO (`DTOs/`) zawiera osobne klasy dla operacji odczytu (`RoomDto`, `ReservationDto`), tworzenia (`CreateRoomDto`, `CreateReservationDto`) i aktualizacji (`UpdateRoomDto`, `UpdateReservationDto`). Warstwa kontrolerów (`Controllers/`) zawiera całą logikę biznesową.

`RoomsController` obsługuje endpoint `/api/rooms` z parametrem `{buildingCode}` pobieranym z trasy oraz filtrami `minCapacity`, `hasProjector` i `activeOnly` z query stringa. `ReservationController` obsługuje `/api/reservation` z filtrami `date`, `status` i `roomId`.

## Walidacja

DTO wejściowe używają Data Annotations: `[Required]` na polach tekstowych, `[MaxLength]` na `Name` i `BuildingCode`, `[Range(1, int.MaxValue)]` na `Capacity`. Walidacja `EndTime > StartTime` sprawdzana jest ręcznie w kontrolerze.

## Reguły biznesowe

Przy tworzeniu rezerwacji kontroler sprawdza kolejno: czy sala istnieje (404 jeśli nie), czy sala jest aktywna (400 jeśli nie), czy nowa rezerwacja nie nakłada się czasowo z istniejącą tego samego dnia (409 jeśli tak). Przy usuwaniu sali sprawdzane jest czy nie ma dla niej przyszłych rezerwacji (409 jeśli są).

## Autor
Hanna Krechyk s32740