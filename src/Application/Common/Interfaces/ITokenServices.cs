using CyberWork.Accounting.Domain.Interfaces;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface ITokenServices
{
    string CreateToken(IAppUser apoUser);
}