using Discord;
using Hanabi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Services;

[Service<ILogService>]
public class LogService(ILogger<LogService> logger) : ILogService
{
    public void DiscordLogWriter(LogMessage message)
    {
        if (message.Exception is not null)
        {
            if(message.Exception.Message.Contains("Expected SocketInteractionContext`1, got SocketInteractionContext"))
                return;
            
            logger.LogError(message.Exception, "{}", message.Exception);
            return;
        }

        switch (message.Severity)
        {
            case LogSeverity.Info:
                logger.LogInformation("{} - {}", message.Source, message.Message);
                break;
            default:
                logger.LogWarning("{} - {}", message.Source, message.Message);
                break;
        }
    }
}