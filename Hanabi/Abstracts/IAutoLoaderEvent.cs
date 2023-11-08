using Discord.WebSocket;

namespace Hanabi.Abstracts;

public interface IAutoLoaderEvent
{
    void RunEvent(DiscordSocketClient client);
}