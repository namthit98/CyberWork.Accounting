using AutoMapper;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;


public class CreateOrganizationCommandHandler
    : IRequestHandler<CreateOrganizationCommand, Result<Guid>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<Guid>> Handle(CreateOrganizationCommand organization,
       CancellationToken cancellationToken)
    {
        var result = await _organizationRepository
            .CreateOrganizationAsync(organization, cancellationToken);

        return Result<Guid>.Success(result);
    }
}