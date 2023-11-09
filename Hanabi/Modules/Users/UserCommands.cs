using Discord;
using Discord.Interactions;
using Hanabi.Extensions;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Users;

[Group("user", "Comandos utils para usuarios")]
[EnabledInDm(false)]
public class UserCommands : InteractionModuleBase
{
    private readonly IEmbedService _embedService;

    public UserCommands(IEmbedService embedService)
    {
        _embedService = embedService;
    }

    [SlashCommand("avatar", "Obtem o avatar de um usuario")]
    public async Task GetAvatar(
        [Summary("usuario",
            "Usuario que deseja obter o avatar, se não preenchido sera pego do usuario atual")]
        IUser? user = null)
    {
        var selectedUser = user ?? Context.User;
        await SendAvatar(selectedUser);
    }

    [UserCommand("Avatar")]
    public async Task UserGetAvatar(IUser user)
    {
        await SendAvatar(user);
    }

    private async ValueTask SendAvatar(IUser user)
    {
        var name = user.GlobalName ?? user.Username;

        var embed = _embedService.GenerateEmbed()
            .WithTitle($"Avatar de {name}")
            .WithImageUrl(user.GetSafeAvatarUrl(1024))
            .Build();

        var controls = new ComponentBuilder()
            .WithButton("Abrir no navegador", style:ButtonStyle.Link,
                url: user.GetSafeAvatarUrl(1024))
            .Build();
        
        await RespondAsync(embed: embed, components:controls);
    }
}