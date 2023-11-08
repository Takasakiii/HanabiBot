using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class SelectMenuEvent : IAutoLoaderEvent
{
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _serviceProvider;

    public SelectMenuEvent(InteractionService interactionService, IServiceProvider serviceProvider)
    {
        _interactionService = interactionService;
        _serviceProvider = serviceProvider;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.SelectMenuExecuted += async component =>
        {
            var ctx = new SocketInteractionContext<SocketMessageComponent>(client, component);
            await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
        };
    }
}