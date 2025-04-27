namespace Domain.Entities;

public class UserRole : EntityBase
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; } = Guid.Parse("1a2feeb0-31e1-47cf-b29a-e18a1d134364");
}