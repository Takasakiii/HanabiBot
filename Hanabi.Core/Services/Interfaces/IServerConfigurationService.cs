using Hanabi.Core.ViewModels;

namespace Hanabi.Core.Services.Interfaces;

public interface IServerConfigurationService
{
    Task<ServerConfigurationViewModel> EditConfig(ServerConfigurationViewModel configs);
    Task<ServerConfigurationViewModel?> GetServerConfig(ulong guildId);
}