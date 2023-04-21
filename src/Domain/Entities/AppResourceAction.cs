namespace CyberWork.Accounting.Domain.Entities;

public class AppResourceAction : BaseAuditableEntity<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; } = Status.Active;
    public virtual IList<UserPermission> UserPermissions { get; set; }
    public virtual IList<RolePermission> RolePermissions { get; set; }
}