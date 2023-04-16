using CyberWork.Accounting.Domain.Enums;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IAppRole
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Status Status { get; set; }
}