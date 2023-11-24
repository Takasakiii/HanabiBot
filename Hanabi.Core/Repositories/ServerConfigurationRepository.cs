using Hanabi.Core.Models;
using Hanabi.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;
using TakasakiStudio.Lina.Database.Repositories;

namespace Hanabi.Core.Repositories;

[Repository<IServerConfigurationRepository>]
public class ServerConfigurationRepository(DbContext dbContext)
    : BaseRepository<ServerConfiguration, ulong>(dbContext), IServerConfigurationRepository;