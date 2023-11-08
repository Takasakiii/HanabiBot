using Hanabi.Core.Models.Validations;
using Hanabi.Core.ViewModels;
using Lina.Database.Models;

namespace Hanabi.Core.Models;

public class ServerConfiguration : BaseValidateBaseEntity<ServerConfiguration, ServerConfigurationValidator, ulong>
{
    public ulong? LogsChatId { get; set; }

    public static implicit operator ServerConfiguration(ServerConfigurationViewModel vm) => new ServerConfiguration
    {
        Id = vm.GuildId,
        LogsChatId = vm.LogsChatId
    };

    public static implicit operator ServerConfigurationViewModel(ServerConfiguration model) =>
        new(model.Id, model.LogsChatId);
}