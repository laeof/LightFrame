using Application.DTO;
using Application.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class HireController : ControllerBase
{
    private readonly IPhotographerRepository photographerRepository;
    public HireController(IPhotographerRepository photographerRepository)
    {
        this.photographerRepository = photographerRepository;
    }
    [HttpPost]
    public async Task<IActionResult> HirePhotographer(PhotographerDto dto)
    {
        var result = await photographerRepository.HirePhotographer(new()
        {
            Email = dto.Email,
            Name = dto.Name,
            PhotoUrl = dto.PhotoUrl,
            WorkExperience = dto.WorkExperience,
        });

        if (result.IsFailure) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> FirePhotographer(Guid id)
    {
        var result = await photographerRepository.FirePhotographer(id);

        if (result.IsFailure) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPhotographers()
    {
        var result = await photographerRepository.GetPhotographers();

        if (result.IsFailure) return BadRequest(result);

        return Ok(result);
    }
}