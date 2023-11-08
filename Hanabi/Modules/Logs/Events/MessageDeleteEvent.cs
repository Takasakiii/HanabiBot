using Discord;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Extensions;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;
using Microsoft.Extensions.Logging;

namespace Hanabi.Modules.Logs.Events;

[Dependency(LifeTime.Transient, typeof(IAutoLoaderEvent))]
public class MessageDeleteEvent : IAutoLoaderEvent
{
    private readonly ILogger<MessageDeleteEvent> _logger;
    private readonly IServerConfigurationService _serverConfigurationService;
    private readonly IEmbedService _embedService;

    public MessageDeleteEvent(
        ILogger<MessageDeleteEvent> logger,
        IServerConfigurationService serverConfigurationService,
        IEmbedService embedService)
    {
        _logger = logger;
        _serverConfigurationService = serverConfigurationService;
        _embedService = embedService;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.MessageDeleted += async (message, channel) =>
        {
            if (!channel.HasValue || channel.Value is not SocketGuildChannel socketChannel)
            {
                _logger.LogInformation("Message {} is not a guild message, skip", message.Id);
                return;
            }


            var botConfig = await _serverConfigurationService.GetServerConfig(socketChannel.Guild.Id);
            if (botConfig?.LogsChatId is null)
            {
                _logger.LogInformation("There are no logs channel in {}", socketChannel.Guild.Id);
                return;
            }

            var logChannel = await client.GetChannelAsync(botConfig.LogsChatId.Value);
            if (logChannel is not ITextChannel logTextChannel)
            {
                _logger.LogWarning("Log channel from guild {} is not a textchannel", socketChannel.Guild.Id);
                return;
            }

            var messageWithOutCache = message.HasValue ? message.Value : null;

            var embed = _embedService.GenerateEmbed()
                .WithTitle("Mensagem Deletada")
                .WithUrl(messageWithOutCache?.GetJumpUrl())
                .WithColor(Color.Red)
                .WithDescription($"Uma mensagem no canal <#{channel.Id}>")
                .WithAuthor(messageWithOutCache?.Author.Username, messageWithOutCache?.Author.GetSafeAvatarUrl())
                .AddField("Antigo conteudo",
                    messageWithOutCache?.Content.CutTheEnd(1024) ?? "Valor antigo indisponivel")
                .Build();

            await logTextChannel.SendMessageAsync(embed: embed);
        };
    }
}