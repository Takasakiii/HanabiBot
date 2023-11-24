using Discord;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Extensions;
using Hanabi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Modules.Logs.Events;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class MessageDeleteEvent(
    ILogger<MessageDeleteEvent> logger,
    IServerConfigurationService serverConfigurationService,
    IEmbedService embedService)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.MessageDeleted += async (cachedMessage, cachedChannel) =>
        {
            var channel = await cachedChannel.GetOrDownloadAsync();
            var message = await cachedMessage.GetOrDownloadAsync();
            
            if (channel is not SocketGuildChannel socketChannel)
            {
                logger.LogInformation("Message {} is not a guild message, skip", message.Id);
                return;
            }


            var botConfig = await serverConfigurationService.GetServerConfig(socketChannel.Guild.Id);
            if (botConfig?.LogsChatId is null)
            {
                logger.LogInformation("There are no logs channel in {}", socketChannel.Guild.Id);
                return;
            }

            var logChannel = await client.GetChannelAsync(botConfig.LogsChatId.Value);
            if (logChannel is not ITextChannel logTextChannel)
            {
                logger.LogWarning("Log channel from guild {} is not a textchannel", socketChannel.Guild.Id);
                return;
            }
            

            var embed = embedService.GenerateEmbed()
                .WithTitle("Mensagem Deletada")
                .WithUrl(message?.GetJumpUrl())
                .WithColor(Color.Red)
                .WithDescription($"Uma mensagem no canal <#{channel.Id}>")
                .WithAuthor(message?.Author.Username, message?.Author.GetSafeAvatarUrl())
                .AddField("Antigo conteudo",
                    message?.Content.CutTheEnd(1024) ?? "Valor antigo indisponivel")
                .Build();

            await logTextChannel.SendMessageAsync(embed: embed);
        };
    }
}