namespace Domain.Entities;

public class UserRole : EntityBase
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}