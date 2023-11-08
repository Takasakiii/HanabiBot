namespace Hanabi.Core.Services.Interfaces;

public interface IHanabiConfig
{
    string DiscordBotToken { get; }
    ulong DiscordParadoxumGuildId { get; }
    string DatabaseConnectionString { get; }
}