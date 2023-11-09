using Hanabi.Core.Services.Interfaces;
using Lina.AutoDependencyInjection;
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

        Console.WriteLine(config.DatabaseConnectionString);
        
        di.AddLogging(builder => builder.AddConsole());
        di.AddLinaDbContext<IHanabiConfig>((options, assembly) => options
            .UseMySql(config.DatabaseConnectionString, ServerVersion.AutoDetect(config.DatabaseConnectionString),
                configMysql => configMysql
                    .MigrationsAssembly(assembly)));
        di.AddAutoDependencyInjection<IHanabiConfig>();
    }

    public static async ValueTask MigrateDatabase(this IServiceProvider provider)
    {
        var dbContext = provider.GetRequiredService<DbContext>();
        await dbContext.Database.MigrateAsync();
    }
}