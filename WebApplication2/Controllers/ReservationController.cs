using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private static List<Room> _rooms => DataStore.Rooms;
    private static List<Reservation> _reservations => DataStore.Reservations;
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Reserwacja o id: {id} nie istnieje");
        }
        return Ok(new ReservationDto()
        {
            Id = reservation.Id,
            RoomId = reservation.RoomId,
            OrganizerName = reservation.OrganizerName,
            Topic = reservation.Topic,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            Status =  reservation.Status
        });
    }
    
    [HttpGet]
    public IActionResult GetByFilter(DateTime? date, string? status, int? roomId)
    {
        var query = _reservations.AsEnumerable();
        if (date is not null)
        {
            query = query.Where(x => x.StartTime.Date == date.Value.Date);
        }

        if (status is not null)
        {
            query = query.Where(x => x.Status == status);
        }

        if (roomId is not null)
        {
            query = query.Where(x => x.RoomId == roomId);
        }

        return Ok(query.Select(
            r=> new ReservationDto()
            {
                Id = r.Id,
                RoomId = r.RoomId,
                OrganizerName = r.OrganizerName,
                Topic = r.Topic,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status
            }).ToList());
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Reservation o id: {id} nie istnieje");
        }
        
        _reservations.Remove(reservation);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateReservationDto reservationDto)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == reservationDto.RoomId);
        if (room is null)
            return NotFound($"Sala o id {reservationDto.RoomId} nie istnieje");
        if (!room.IsActive)
        {
            return BadRequest($"Sala o id {reservationDto.RoomId} jest nieaktywna");
        }
        bool hasConflict = _reservations.Any(r =>
            r.RoomId == reservationDto.RoomId &&
            r.StartTime.Date == reservationDto.StartTime.Date &&
            r.StartTime < reservationDto.EndTime &&
            r.EndTime > reservationDto.StartTime
        );
        if (hasConflict)
            return Conflict("Rezerwacja koliduje z istniejącą rezerwacją tej sali");
        var reservation = new Reservation()
        {
            Id = _reservations.Max(e => e.Id) + 1,
            RoomId = reservationDto.RoomId,
            Topic =  reservationDto.Topic,
            OrganizerName = reservationDto.OrganizerName,
            StartTime = reservationDto.StartTime,
            EndTime = reservationDto.EndTime,
            Status = reservationDto.Status
        };
        
        _reservations.Add(reservation);
        
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateReservationDto reservationDto)
    {
        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        
        if (reservation is null)
        {
            return NotFound($"Rezerwacja o id: {id} nie istnieje");
        }

        if (reservationDto.StartTime > reservationDto.EndTime)
        {
            return BadRequest("StartTime must be before EndTime");
        }
        
        reservation.RoomId = reservationDto.RoomId;
        reservation.Topic = reservationDto.Topic;
        reservation.OrganizerName = reservationDto.OrganizerName;
        reservation.StartTime = reservationDto.StartTime;
        reservation.EndTime = reservationDto.EndTime;
        reservation.Status = reservationDto.Status;
        
        return NoContent();
    }
}