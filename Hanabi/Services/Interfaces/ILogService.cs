using Discord;

namespace Hanabi.Services.Interfaces;

public interface ILogService
{
    void DiscordLogWriter(LogMessage message);
}