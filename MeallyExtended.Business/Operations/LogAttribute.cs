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
        var userParameter = meta.Target.Method.Parameters.FirstOrDefault(x => x.Name == "userEmail");

        _logger.LogInformation(userParameter is not null
            ? $"{meta.Target.Method} called by {userParameter.Value} | {DateTime.Now}"
            : $"{meta.Target.Method} called | {DateTime.Now}");

        try
        {
            var result = meta.Proceed();

            _logger.LogInformation($"{meta.Target.Method} succeeded | {DateTime.Now}");

            return result;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"{meta.Target.Method} failed: {e.Message} | {DateTime.Now}");

            throw;
        }
    }
}

