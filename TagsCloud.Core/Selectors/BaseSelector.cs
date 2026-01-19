using TagsCloud.Core.Models;
using TagsCloud.Core.Services;

namespace TagsCloud.Core.Selectors;

public abstract class BaseSelector<T> where T : INamedService
{
    private readonly Dictionary<string, T> services;

    public BaseSelector(IEnumerable<T> services)
    {
        this.services = services.ToDictionary(s => s.Name);
    }

    public Result<T> Select(string serviceName, string parameterName)
    {
        if (!services.TryGetValue(serviceName, out var service))
            return Result<T>.Fail($"Параметр \"{parameterName}: {serviceName}\" не валиден");

        return Result<T>.Ok(service);
    }
}