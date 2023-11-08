using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.GlobalEvents;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class InteractionsHandlerLogsEvent : IAutoLoaderEvent
{
    private readonly InteractionService _interactionService;
    private readonly ILogService _logService;

    public InteractionsHandlerLogsEvent(InteractionService interactionService, ILogService logService)
    {
        _interactionService = interactionService;
        _logService = logService;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        _interactionService.Log += async message =>
        {
            _logService.DiscordLogWriter(message);
            await Task.CompletedTask;
        };
    }
}