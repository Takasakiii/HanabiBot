using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class InteractionCreatedEvent(InteractionService interactionService, IServiceProvider serviceProvider)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.InteractionCreated += async interaction =>
        {
            var ctx = new SocketInteractionContext(client, interaction);
            await interactionService.ExecuteCommandAsync(ctx, serviceProvider);
        };
    }
}