using Hanabi.Core.Services.Interfaces;
using Lina.LoaderConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hanabi.Core;

public static class HanabiCore
{
    public static void AddHanabiCore(this IServiceCollection di)
    {
        di.AddLoaderConfig<IHanabiConfig>();
        di.AddLogging(builder => builder.AddConsole());
    }
}