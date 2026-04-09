using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private static List<Animal> _animals = [
        new Animal()
        {
            Id = 1,
            Name = "Batman",
            Species = "Batman",
            Weight = 100.0,
        },
        new Animal()
        {
            Id = 2,
            Name = "Pimpek",
            Species = "Pies",
            Weight = 100.0,
        }
    ];

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_animals.Select(e => new AnimalDto
        {
            Id = e.Id,
            Name = e.Name,
            Species = e.Species,
        }));
    }

    [HttpGet("{id:int}")] // api/[controller]/10
    public IActionResult GetById(int id)
    {
        var animal = _animals.FirstOrDefault(x => x.Id == id);
        
        if (animal is null)
        {
            return NotFound($"Zwierze o id: {id} nie istnieje");
        }
        
        return Ok(new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Species = animal.Species,
        });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var animal = _animals.FirstOrDefault(x => x.Id == id);
        
        if (animal is null)
        {
            return NotFound($"Zwierze o id: {id} nie istnieje");
        }
        
        _animals.Remove(animal);
        return NoContent();
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateAnimalDto animalDto)
    {
        var animal = new Animal
        {
            Id = _animals.Max(e => e.Id) + 1,
            Name = animalDto.Name,
            Species = animalDto.Species,
            Weight = animalDto.Weight,
        };
        
        _animals.Add(animal);
        
        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateAnimalDto animalDto)
    {
        var animal = _animals.FirstOrDefault(x => x.Id == id);
        
        if (animal is null)
        {
            return NotFound();
        }
        
        animal.Name = animalDto.Name;
        animal.Species = animalDto.Species;
        animal.Weight = animalDto.Weight;
        
        return NoContent();
    }
}