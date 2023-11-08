using Discord;
using Discord.WebSocket;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Services;

[Service(typeof(IEmbedService))]
public class EmbedService : IEmbedService
{
    private readonly DiscordSocketClient _client;

    public EmbedService(DiscordSocketClient client)
    {
        _client = client;
    }

    public EmbedBuilder GenerateEmbed()
    {
        return new EmbedBuilder()
            .WithColor(Color.DarkPurple)
            .WithFooter(_client.CurrentUser.Username, _client.CurrentUser.GetAvatarUrl())
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