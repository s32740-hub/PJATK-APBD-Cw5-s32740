using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOs;

public class CreateAnimalDto
{
    [Required]
    [MaxLength(15)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(15)]
    public string Species { get; set; } = string.Empty;
    [Required]
    public double Weight { get; set; }
}