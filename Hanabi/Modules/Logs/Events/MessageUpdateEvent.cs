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
public class MessageUpdateEvent : IAutoLoaderEvent
{
    private readonly IServerConfigurationService _serverConfigurationService;
    private readonly ILogger<MessageUpdateEvent> _logger;
    private readonly IEmbedService _embedService;

    public MessageUpdateEvent(
        IServerConfigurationService serverConfigurationService,
        ILogger<MessageUpdateEvent> logger,
        IEmbedService embedService)
    {
        _serverConfigurationService = serverConfigurationService;
        _logger = logger;
        _embedService = embedService;
    }

    public void RunEvent(DiscordSocketClient client)
    {
        client.MessageUpdated += async (cacheable, message, channel) =>
        {
            if (channel is not SocketGuildChannel socketChannel)
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

            var embed = _embedService.GenerateEmbed()
                .WithTitle("Mensagem Alterada")
                .WithUrl(message.GetJumpUrl())
                .WithColor(Color.Gold)
                .WithDescription($"Uma mensagem no canal <#{channel.Id}>")
                .WithAuthor(message.Author.Username, message.Author.GetSafeAvatarUrl())
                .AddField("Antigo conteudo", cacheable.HasValue ? cacheable.Value.Content.CutTheEnd(1024) : "Valor antigo indisponivel")
                .AddField("Novo conteudo", message.Content.CutTheEnd(1024))
                .Build();

            await logTextChannel.SendMessageAsync(embed: embed);
        };
    }
}