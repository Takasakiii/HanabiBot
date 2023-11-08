using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class InteractionCreatedEvent : IAutoLoaderEvent
{
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _serviceProvider;

    public InteractionCreatedEvent(InteractionService interactionService, IServiceProvider serviceProvider)
    {
        _interactionService = interactionService;
        _serviceProvider = serviceProvider;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.InteractionCreated += async interaction =>
        {
            var ctx = new SocketInteractionContext(client, interaction);
            await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
        };
    }
}