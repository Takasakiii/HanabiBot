using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class ReadEvent(IHanabiConfig config, InteractionService interactionService, ILogger<ReadEvent> logger)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.Ready += async () =>
        {
            var guild = client.Guilds.First(x => x.Id == config.DiscordParadoxumGuildId);
            
            var commands = await interactionService.RegisterCommandsToGuildAsync(guild.Id);
            logger.LogInformation("Registered {} commands in guild {}", commands.Count,
                guild.Name);
        };
    }
}