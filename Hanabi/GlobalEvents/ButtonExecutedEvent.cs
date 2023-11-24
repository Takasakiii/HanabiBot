using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
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