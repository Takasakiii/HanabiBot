using Hanabi.Core.Models;
using Hanabi.Core.Repositories.Interfaces;
using Lina.AutoDependencyInjection.Attributes;
using Lina.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hanabi.Core.Repositories;

[Repository(typeof(IServerConfigurationRepository))]
public class ServerConfigurationRepository : BaseRepository<ServerConfiguration, ulong>, IServerConfigurationRepository
{
    public ServerConfigurationRepository(DbContext dbContext) : base(dbContext)
    {
    }
}