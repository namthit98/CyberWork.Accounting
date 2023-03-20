namespace CyberWork.Accounting.Domain.Events;

public class OrganizationCreatedEvent : BaseEvent
{
    public OrganizationCreatedEvent(Organization organization)
    {
        Organization = organization;
    }

    public Organization Organization { get; }
}