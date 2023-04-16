namespace CyberWork.Accounting.Domain.Entities;

public class UserProfile : BaseAuditableEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalEmail { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
}