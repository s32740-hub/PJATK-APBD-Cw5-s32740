using WebApplication2.Models;

namespace WebApplication2;

public class DataStore
{
    public static List<Reservation> Reservations { get; } = [
        new Reservation()
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Jan Kowalski",
            Topic = "Spotkanie projektowe - Nowe API",
            StartTime = new DateTime(2026, 4, 10, 10, 0, 0),
            EndTime = new DateTime(2026, 4, 10, 11, 30, 0),
            Status = "Confirmed"
        },
        new Reservation()
        {
            Id = 2,
            RoomId = 3,
            OrganizerName = "Anna Nowak",
            Topic = "Szkolenie z ASP.NET Core",
            StartTime = new DateTime(2026, 4, 11, 9, 0, 0),
            EndTime = new DateTime(2026, 4, 11, 15, 0, 0),
            Status = "Confirmed"
        },
        new Reservation()
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "Piotr Wiśniewski",
            Topic = "Rozmowa rekrutacyjna (Mid Backend Developer)",
            StartTime = new DateTime(2026, 4, 12, 14, 0, 0),
            EndTime = new DateTime(2026, 4, 12, 15, 0, 0),
            Status = "Pending"
        },
        new Reservation()
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Katarzyna Wójcik",
            Topic = "Przegląd wyników kwartalnych",
            StartTime = new DateTime(2026, 4, 14, 12, 0, 0),
            EndTime = new DateTime(2026, 4, 14, 14, 0, 0),
            Status = "Confirmed"
        },
        new Reservation()
        {
            Id = 5,
            RoomId = 1,
            OrganizerName = "Michał Kamiński",
            Topic = "Burza mózgów - Architektura",
            StartTime = new DateTime(2026, 4, 15, 10, 0, 0),
            EndTime = new DateTime(2026, 4, 15, 12, 0, 0),
            Status = "Cancelled"
        },
        new Reservation()
        {
            Id = 6,
            RoomId = 4,
            OrganizerName = "Zespół IT",
            Topic = "Nieformalne spotkanie integracyjne",
            StartTime = new DateTime(2026, 4, 16, 15, 0, 0),
            EndTime = new DateTime(2026, 4, 16, 16, 0, 0),
            Status = "Confirmed"
        }
    ];
    
    public static List<Room> Rooms { get; } = [
        new Room()
        {
            Id = 1,
            Name = "Sala Neptun",
            BuildingCode = "B1",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room()
        {
            Id = 2,
            Name = "Sala Mars",
            BuildingCode = "B1",
            Floor = 2,
            Capacity = 10,
            HasProjector = false,
            IsActive = true
        },
        new Room()
        {
            Id = 3,
            Name = "Lab 04",
            BuildingCode = "TECH",
            Floor = 0,
            Capacity = 50,
            HasProjector = true,
            IsActive = true
        },
        new Room()
        {
            Id = 4,
            Name = "Kącik Kawowy",
            BuildingCode = "B2",
            Floor = 1,
            Capacity = 6,
            HasProjector = false,
            IsActive = false
        },
        new Room()
        {
            Id = 5,
            Name = "Sala Zarządu",
            BuildingCode = "HQ",
            Floor = 10,
            Capacity = 12,
            HasProjector = true,
            IsActive = true
        }
    ];
}