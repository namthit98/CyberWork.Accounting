using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler
    : IRequestHandler<DeleteOrganizationCommand, Result<Guid>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    public async Task<Result<Guid>> Handle(DeleteOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _organizationRepository
            .DeleteOrganizationAsync(request.Id, cancellationToken);

        return Result<Guid>.Success(result);
    }
}