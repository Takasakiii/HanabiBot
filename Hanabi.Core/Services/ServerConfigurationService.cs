using Hanabi.Core.Models;
using Hanabi.Core.Repositories.Interfaces;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Core.Services;

[Service(typeof(IServerConfigurationService))]
public class ServerConfigurationService : IServerConfigurationService
{
    private readonly IServerConfigurationRepository _configurationRepository;

    public ServerConfigurationService(IServerConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public async Task<ServerConfigurationViewModel> EditConfig(ServerConfigurationViewModel configs)
    {
        ServerConfiguration configModel = configs;
        
        var transaction = await _configurationRepository.BeginTransaction();
        if (await _configurationRepository.GetById(configs.GuildId) is { } currentSettings)
        {
            _configurationRepository.Delete(currentSettings);
            await _configurationRepository.Commit();
            _configurationRepository.Detach(currentSettings);
        }


        await _configurationRepository.Add(configModel);
        await _configurationRepository.Commit();
        
        await transaction.CommitAsync();

        return configModel;
    }

    public async Task<ServerConfigurationViewModel?> GetServerConfig(ulong guildId)
    {
        var config = await _configurationRepository.GetById(guildId);
        if (config is null) return null;

        return config;
    }
}