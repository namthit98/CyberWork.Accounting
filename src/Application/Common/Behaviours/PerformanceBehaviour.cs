using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CyberWork.Accounting.Application.Common.Behaviours;


public class PerformanceBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    // private readonly ICurrentUserService _currentUserService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger
    // ICurrentUserService currentUserService
    )
    {
        _timer = new Stopwatch();
        _logger = logger;
        // _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            // var userId = _currentUserService.UserId ?? string.Empty;
            // var userName = string.Empty;

            // if (!string.IsNullOrEmpty(userId))
            // {
            //     userName = await _identityService.GetUserNameAsync(userId);
            // }

             _logger.LogWarning("CyberWork.Accounting Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);

        }

        return response;
    }
}
