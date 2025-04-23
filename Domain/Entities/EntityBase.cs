namespace Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set;}

    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public bool IsDisabled { get; set; }
}