using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CyberWork.Accounting.Application.Common.Behaviours;


public class LoggingBehaviour<TRequest>
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    // private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger
    // ICurrentUserService currentUserService
    )
    {
        _logger = logger;
        // _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        // var userId = _currentUserService.UserId ?? string.Empty;
        // string? userName = string.Empty;

        // if (!string.IsNullOrEmpty(userId))
        // {
        //     userName = await _identityService.GetUserNameAsync(userId);
        // }


        _logger.LogInformation("CyberWork.Accounting Request: {Name} {@Request}",
            requestName, request);

        await Task.CompletedTask;
    }
}
