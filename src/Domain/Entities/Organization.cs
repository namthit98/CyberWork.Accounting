
namespace CyberWork.Accounting.Domain.Entities;

public class Organization : BaseAuditableEntity<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public Guid UnderOrganizationId { get; set; }
    public string OrganizationLevel { get; set; }
    public string Address { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Active;
}
