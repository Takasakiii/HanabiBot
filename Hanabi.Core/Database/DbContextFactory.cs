using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Hanabi.Core.Database;

public class DbContextFactory : IDesignTimeDbContextFactory<DbContext>
{
    public DbContext CreateDbContext(string[] args)
    {
        var di = new ServiceCollection();
        di.AddHanabiCore();
        return di.BuildServiceProvider()
            .GetRequiredService<DbContext>();
    }
}