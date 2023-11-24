using Discord;
using Discord.WebSocket;
using Hanabi.Abstracts;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Extensions;
using Hanabi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Modules.Starboard.Events;

[Dependency<IAutoLoaderEvent>(LifeTime.Transient)]
public class ReactionAddedEvent(
    IServerConfigurationService serverConfigurationService,
    ILogger<ReactionAddedEvent> logger,
    IEmbedService embedService)
    : IAutoLoaderEvent
{
    public void RunEvent(DiscordSocketClient client)
    {
        client.ReactionAdded += async (cachedMessage, cachedChannel, reaction) =>
        {
            if(reaction.Emote.Name != new Emoji("\u2b50").Name)
                return;

            var channel = await cachedChannel.GetOrDownloadAsync();
            var message = await cachedMessage.GetOrDownloadAsync();
            
            if (channel is null || message is null)
                return;
            
            
            if (message.Reactions.TryGetValue(new Emoji("🌟"), out var star2Reaction) && star2Reaction.IsMe)
                return;
            
            if(channel is not SocketTextChannel socketTextChannel)
                return;

            var configs = await serverConfigurationService.GetServerConfig(socketTextChannel.Guild.Id);

            if (configs?.StarBoardChannel is null || configs.StarBoardMinimalStars is null)
            {
                logger.LogInformation("Possible malformed starboard in guild {}", socketTextChannel.Guild.Id);
                return;
            }
            
            if(socketTextChannel.Id == configs.StarBoardChannel)
                return;
            
            if(message.Reactions[reaction.Emote].ReactionCount < configs.StarBoardMinimalStars)
                return;

            var starboardChannel = await client.GetChannelAsync(configs.StarBoardChannel.Value);

            if (starboardChannel is not ITextChannel starboardTextChannel)
            {
                logger.LogWarning("Starboard channel does not exist in guild {}", socketTextChannel.Guild.Id);
                return;
            }

            var mainAttachment = message.Attachments.FirstOrDefault();

            var embed = embedService.GenerateEmbed()
                .WithAuthor(message.Author.GlobalName ?? message.Author.Username,
                    message.Author.GetSafeAvatarUrl())
                .WithColor(Color.Gold)
                .WithTitle("\u2b50 Starboard \u2b50")
                .WithDescription(message.Content.CutTheEnd());
            
            if (mainAttachment?.ContentType.StartsWith("image/") == true)
            {
                embed.WithImageUrl(mainAttachment.Url);
            }

            await starboardTextChannel.SendMessageAsync(embed: embed.Build());
            await message.AddReactionAsync(new Emoji("\ud83c\udf1f"));
        };
    }
}