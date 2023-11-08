using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class ButtonExecutedEvent : IAutoLoaderEvent
{
    private readonly IServiceProvider _serviceProvider;
    private readonly InteractionService _interactionService;

    public ButtonExecutedEvent(IServiceProvider serviceProvider, InteractionService interactionService)
    {
        _serviceProvider = serviceProvider;
        _interactionService = interactionService;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.ButtonExecuted += async component =>
        {
            var ctx = new SocketInteractionContext<SocketMessageComponent>(client, component);
            await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
        };
    }
}