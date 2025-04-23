using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;

namespace Application.UseCases;

public class HireUseCase
{
    private readonly IPhotographerRepository photographerRepository;
    public HireUseCase(IPhotographerRepository photographerRepository)
    {
        this.photographerRepository = photographerRepository;
    }

    public async Task<IResult<Photographer>> ExecuteAsync(PhotographerDto dto)
    {
        var result = await photographerRepository.HirePhotographer(new()
        {
            Email = dto.Email,
            PhotoUrl = dto.PhotoUrl,
            Name = dto.Name,
            WorkExperience = dto.WorkExperience,
        });

        if (result.IsFailure) return result;

        return result;
    }
}