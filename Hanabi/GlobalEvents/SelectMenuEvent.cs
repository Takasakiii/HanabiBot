using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class SelectMenuEvent(InteractionService interactionService, IServiceProvider serviceProvider)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.SelectMenuExecuted += async component =>
        {
            var ctx = new SocketInteractionContext<SocketMessageComponent>(client, component);
            await interactionService.ExecuteCommandAsync(ctx, serviceProvider);
        };
    }
}