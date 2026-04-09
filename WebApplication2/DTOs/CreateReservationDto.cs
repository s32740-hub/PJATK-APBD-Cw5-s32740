using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOs;

public class CreateReservationDto
{
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; }
    [Required]
    public string Topic { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }
}