using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;

public class UpdateStatusOrganizationCommandHandler
    : IRequestHandler<UpdateStatusOrganizationCommand, Result<Guid>>
{

    private readonly IOrganizationRepository _organizationRepository;

    public UpdateStatusOrganizationCommandHandler(
        IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    public async Task<Result<Guid>> Handle(UpdateStatusOrganizationCommand organization,
      CancellationToken cancellationToken)
    {
        var result = await _organizationRepository
            .UpdateStatusOrganizationAsync(organization, cancellationToken);

        return Result<Guid>.Success(result);
    }
}