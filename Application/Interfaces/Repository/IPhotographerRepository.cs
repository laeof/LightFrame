using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IPhotographerRepository
{
    public Task<IResult<Photographer>> HirePhotographer(Photographer photographer);
    public Task<IResult<Photographer>> FirePhotographer(Guid id);
}