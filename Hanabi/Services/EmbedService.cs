using Discord;
using Discord.WebSocket;
using Hanabi.Services.Interfaces;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Services;

[Service<IEmbedService>]
public class EmbedService(DiscordSocketClient client) : IEmbedService
{
    public EmbedBuilder GenerateEmbed()
    {
        return new EmbedBuilder()
            .WithColor(Color.DarkPurple)
            .WithFooter(client.CurrentUser.Username, client.CurrentUser.GetAvatarUrl())
            .WithTimestamp(DateTimeOffset.Now);
    }

    public Embed GenerateSuccessEmbed(string message)
    {
        return GenerateEmbed()
            .WithColor(Color.Green)
            .WithTitle("Sucesso")
            .WithDescription($"\u2705 {message}")
            .Build();
    }
    
    public Embed GenerateErrorEmbed(string message)
    {
        return GenerateEmbed()
            .WithColor(Color.Red)
            .WithTitle("Erro")
            .WithDescription($"\u2757 {message}")
            .Build();
    }
}