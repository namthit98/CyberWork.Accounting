using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler
    : IRequestHandler<UpdateOrganizationCommand, Result<Guid>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateOrganizationCommand organization,
       CancellationToken cancellationToken)
    {
        var result = await _organizationRepository
            .UpdateOrganizationAsync(organization, cancellationToken);

        return Result<Guid>.Success(result);
    }
}