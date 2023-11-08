namespace Hanabi.Core.ViewModels;

public record ServerConfigurationViewModel(ulong GuildId)
{
    public ulong? LogsChatId { get; set; }
    public ulong? StarBoardChannel { get; set; }
    public uint? StarBoardMinimalStars { get; set; }
}