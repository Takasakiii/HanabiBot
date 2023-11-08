using Hanabi.Core.Services.Interfaces;
using Lina.Database;
using Lina.LoaderConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hanabi.Core;

public static class HanabiCore
{
    public static void AddHanabiCore(this IServiceCollection di)
    {
        var config = di.AddLoaderConfig<IHanabiConfig>();
        di.AddLogging(builder => builder.AddConsole());
        di.AddLinaDbContext<IHanabiConfig>((options, assembly) => options
            .UseMySql(config.DatabaseConnectionString, ServerVersion.AutoDetect(config.DatabaseConnectionString),
                configMysql => configMysql
                    .MigrationsAssembly(assembly)));
    }
}