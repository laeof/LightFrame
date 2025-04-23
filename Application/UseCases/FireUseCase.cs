using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;

namespace Application.UseCases;

public class FireUseCase
{
    private readonly IPhotographerRepository photographerRepository;
    public FireUseCase(IPhotographerRepository photographerRepository)
    {
        this.photographerRepository = photographerRepository;
    }

    public async Task<IResult<Photographer>> ExecuteAsync(Guid id)
    {
        var result = await photographerRepository.FirePhotographer(id);

        if (result.IsFailure) return result;

        return result;
    }
}