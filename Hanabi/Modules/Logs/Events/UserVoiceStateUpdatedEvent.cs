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
public class UserVoiceStateUpdatedEvent(
    IServerConfigurationService serverConfigurationService,
    ILogger<UserVoiceStateUpdatedEvent> logger,
    IEmbedService embedService) 
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.UserVoiceStateUpdated += async (user, _, newState) =>
        {
            if (newState.VoiceChannel is null)
                return;

            var botConfig = await serverConfigurationService.GetServerConfig(newState.VoiceChannel.Guild.Id);
            if (botConfig?.LogsChatId is null)
            {
                logger.LogInformation("There are no logs channel in {}", newState.VoiceChannel.Guild.Id);
                return;
            }

            var logChannel = await client.GetChannelAsync(botConfig.LogsChatId.Value);
            if (logChannel is not ITextChannel logTextChannel)
            {
                logger.LogWarning("Log channel from guild {} is not a textchannel", botConfig.LogsChatId);
                return;
            }

            var embed = embedService.GenerateEmbed()
                .WithTitle("Entrada em canal de voz")
                .WithColor(Color.Green)
                .WithDescription($"Ocorreu uma entrada no canal <#{newState.VoiceChannel.Id}>")
                .WithAuthor(user.Username, user.GetSafeAvatarUrl())
                .Build();

            await logTextChannel.SendMessageAsync(embed: embed);
        };
    }
}