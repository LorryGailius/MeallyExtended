using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Microsoft.Extensions.Logging;

namespace MeallyExtended.Business.Operations;

[Serializable]
public class LogAttribute : OverrideMethodAspect
{

    [IntroduceDependency]
    private readonly ILogger _logger;

    public override dynamic? OverrideMethod()
    {
        _logger.LogInformation($"{meta.Target.Method} started.");

        try
        {
            var result = meta.Proceed();

            _logger.LogInformation($"{meta.Target.Method} succeeded.");

            return result;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"{meta.Target.Method} failed: {e.Message}.");

            throw;
        }
    }
}

