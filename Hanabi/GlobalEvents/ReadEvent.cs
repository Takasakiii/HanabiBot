using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;
using Microsoft.Extensions.Logging;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class ReadEvent : IAutoLoaderEvent
{
    private readonly IHanabiConfig _config;
    private readonly InteractionService _interactionService;
    private readonly ILogger<ReadEvent> _logger;

    public ReadEvent(IHanabiConfig config, InteractionService interactionService, ILogger<ReadEvent> logger)
    {
        _config = config;
        _interactionService = interactionService;
        _logger = logger;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.Ready += async () =>
        {
            var guild = client.Guilds.First(x => x.Id == _config.DiscordParadoxumGuildId);
            
            var commands = await _interactionService.RegisterCommandsToGuildAsync(guild.Id);
            _logger.LogInformation("Registered {} commands in guild {}", commands.Count,
                guild.Name);
        };
    }
}