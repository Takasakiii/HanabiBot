using Hanabi.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.Database;
using TakasakiStudio.Lina.Utils.LoaderConfig;

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
        di.AddAutoDependencyInjection<IHanabiConfig>();
        var appSettingsConfig = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        di.AddSingleton<IConfiguration>(appSettingsConfig);
    }

    public static async ValueTask MigrateDatabase(this IServiceProvider provider)
    {
        var dbContext = provider.GetRequiredService<DbContext>();
        await dbContext.Database.MigrateAsync();
    }
}