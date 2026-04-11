using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static List<Room> _rooms => DataStore.Rooms;
    private static List<Reservation> _reservations => DataStore.Reservations;
    [HttpGet("{id:int}")] // api/[controller]/10
    public IActionResult GetById(int id)
    {
        var room = _rooms.FirstOrDefault(x => x.Id == id);
        
        if (room is null)
        {
            return NotFound($"Sala o id: {id} nie istnieje");
        }
        return Ok(new RoomDto()
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        });
    }
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        List<Room> rooms = _rooms.Where(x => x.BuildingCode == buildingCode).ToList();
        
        if (rooms.Count == 0)
        {
            return NotFound($"Nie ma sal w budynku: {buildingCode}");
        }
        return Ok(rooms.Select(x => new RoomDto{
            Id = x.Id,
            Name = x.Name,
            BuildingCode = x.BuildingCode,
            Floor = x.Floor,
            Capacity = x.Capacity,
            IsActive = x.IsActive,
            HasProjector = x.HasProjector
        }));
    }
    
    [HttpGet]
    public IActionResult Get(int? minCapacity, bool? hasProjector, bool activeOnly=false)
    {
        var query = _rooms.AsEnumerable();
        if (minCapacity is not null)
        {
            query = query.Where(x => x.Capacity >= minCapacity);
        }

        if (hasProjector is not null)
        {
            query = query.Where(x => x.HasProjector == hasProjector);
        }

        if (activeOnly)
        {
            query = query.Where(x => x.IsActive == true);
        }

        return Ok(
            query.Select(
                r => new RoomDto()
                {
                    Id = r.Id,
                    Name = r.Name,
                    BuildingCode = r.BuildingCode,
                    Floor = r.Floor,
                    Capacity = r.Capacity,
                    IsActive = r.IsActive,
                    HasProjector = r.HasProjector,
                }
                ));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = _rooms.FirstOrDefault(x => x.Id == id);
        
        if (room is null)
        {
            return NotFound($"Sala o id: {id} nie istnieje");
        }
        var res = _reservations.Any(r =>
            r.RoomId == id && r.StartTime > DateTime.Now
        );
        if (res)
        {
            return Conflict("Nie można usunąć sali z przyszłymi rezerwacjami");
        }
        
        _rooms.Remove(room);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomDto roomDto)
    {
        var room = new Room
        {
            Id = (_rooms.Any()?_rooms.Max(e => e.Id):0) + 1,
            Name = roomDto.Name,
            BuildingCode = roomDto.BuildingCode,
            Floor = roomDto.Floor,
            Capacity = roomDto.Capacity,
            IsActive = roomDto.IsActive,
            HasProjector = roomDto.HasProjector
        };
        
        _rooms.Add(room);
        
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateRoomDto roomDto)
    {
        var room = _rooms.FirstOrDefault(x => x.Id == id);
        
        if (room is null)
        {
            return NotFound();
        }
        
        room.Name = roomDto.Name;
        room.BuildingCode = roomDto.BuildingCode;
        room.Floor = roomDto.Floor;
        room.Capacity = roomDto.Capacity;
        room.IsActive = roomDto.IsActive;
        room.HasProjector = roomDto.HasProjector;
        
        
        return NoContent();
    }
}