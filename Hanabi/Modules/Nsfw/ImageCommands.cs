using Discord;
using Discord.Interactions;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Nsfw;

[Group("images", "Comandos de imagem que podem ser +18")]
[EnabledInDm(false)]
[NsfwCommand(true)]
public class ImageCommands(IImageService imageService, IEmbedService embedService) : InteractionModuleBase
{
    [SlashCommand("random", "Envia uma imagem randomicamente")]
    public async Task Random([Summary("Tags", "Tags para obter a imagem")] string? tags = null)
    {
        try
        {
            await DeferAsync();
            var randomImage = await imageService.GetRandomImage(tags);

            var embed = GenerateImageEmbed(randomImage)
                .WithTitle("Aqui esta sua imagem random!")
                .Build();

            await FollowupAsync(embed: embed);
        }
        catch
        {
            await FollowupAsync(embed: embedService.GenerateErrorEmbed("Nenhuma imagem encontrada"));
        }
    }
    
    [SlashCommand("random-unsafe", "Envia uma imagem randomicamente")]
    public async Task RandomCompleteUnsafe([Summary("Tags", "Tags para obter a imagem")] string? tags = null)
    {
        try
        {
            await DeferAsync();
            var randomImage = await imageService.GetRandomImage($"{tags} -rating:s");

            var embed = GenerateImageEmbed(randomImage)
                .WithTitle("Aqui esta sua imagem depravada!")
                .Build();

            await FollowupAsync(embed: embed);
        }
        catch
        {
            await FollowupAsync(embed: embedService.GenerateErrorEmbed("Nenhuma imagem encontrada"));
        }
        
    }

    private EmbedBuilder GenerateImageEmbed(GelbooruPostViewModel image) => embedService.GenerateEmbed()
        .WithDescription($"Essa imagem possui as tags:\n\n{image.Tags}")
        .WithUrl(image.FileUrl)
        .WithImageUrl(image.FileUrl);
}