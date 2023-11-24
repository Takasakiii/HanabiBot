using Discord.Interactions;

namespace Hanabi.Modules.Nsfw;

[Group("nsfw", "Comandos decepsionantes")]
[EnabledInDm(false)]
[NsfwCommand(true)]
public class NsfwCommands : InteractionModuleBase
{
    [SlashCommand("hentai", "Envia uma imagem decepsionante")]
    public async Task Hentai()
    {
        
    }
}