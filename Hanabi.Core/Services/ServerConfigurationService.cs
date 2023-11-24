using Hanabi.Core.Models;
using Hanabi.Core.Repositories.Interfaces;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Core.Services;

[Service<IServerConfigurationService>]
public class ServerConfigurationService(IServerConfigurationRepository configurationRepository)
    : IServerConfigurationService
{
    public async Task<ServerConfigurationViewModel> EditConfig(ServerConfigurationViewModel configs)
    {
        ServerConfiguration configModel = configs;
        
        var transaction = await configurationRepository.BeginTransaction();
        if (await configurationRepository.GetById(configs.GuildId) is { } currentSettings)
        {
            configurationRepository.Delete(currentSettings);
            await configurationRepository.Commit();
            configurationRepository.Detach(currentSettings);
        }


        await configurationRepository.Add(configModel);
        await configurationRepository.Commit();
        
        await transaction.CommitAsync();

        return configModel;
    }

    public async Task<ServerConfigurationViewModel?> GetServerConfig(ulong guildId)
    {
        var config = await configurationRepository.GetById(guildId);
        if (config is null) return null;

        return config;
    }
}