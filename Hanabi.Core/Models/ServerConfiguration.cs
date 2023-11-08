using Hanabi.Core.ViewModels;
using Lina.Database.Models;

namespace Hanabi.Core.Models;

public class ServerConfiguration : BaseEntity<ulong>
{
    public ulong? LogsChatId { get; set; }
    public ulong? StarBoardChannel { get; set; }
    public uint? StarBoardMinimalStars { get; set; }

    public static implicit operator ServerConfiguration(ServerConfigurationViewModel vm) => new()
    {
        Id = vm.GuildId,
        LogsChatId = vm.LogsChatId,
        StarBoardChannel = vm.StarBoardChannel,
        StarBoardMinimalStars = vm.StarBoardMinimalStars
    };

    public static implicit operator ServerConfigurationViewModel(ServerConfiguration model) =>
        new(model.Id)
        {
            LogsChatId = model.LogsChatId, 
            StarBoardMinimalStars = model.StarBoardMinimalStars, 
            StarBoardChannel = model.StarBoardChannel
        };
}