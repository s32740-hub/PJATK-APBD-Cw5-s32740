using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOs;

public class UpdateRoomDto
{

    [Required] [MaxLength(15)] public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(10)] public string BuildingCode { get; set; } = string.Empty;
    [Required]
    public int Floor { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; } = false;
    public bool IsActive { get; set; } = false;
}