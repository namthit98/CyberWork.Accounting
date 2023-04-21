namespace CyberWork.Accounting.Domain.Entities;

public class UserPermission : BaseAuditableEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid AppResourceActionId { get; set; }
    public virtual AppResourceAction AppResourceAction { get; set; }
    public Guid AppResourceId { get; set; }
    public virtual AppResource AppResource { get; set; }
    public Status Status { get; set; } = Status.Active;
}