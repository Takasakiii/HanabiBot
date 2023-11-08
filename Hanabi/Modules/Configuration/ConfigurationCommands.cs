using Discord;
using Discord.Interactions;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Configuration;

[Group("config", "Configura as ações da Hanabi no Server")]
[EnabledInDm(false)]
[DefaultMemberPermissions(GuildPermission.Administrator)]
public class ConfigurationCommands : InteractionModuleBase
{
    private readonly IEmbedService _embedService;

    public ConfigurationCommands(IEmbedService embedService)
    {
        _embedService = embedService;
    }

    [SlashCommand("logs", "Configura o modulo de logs")]
    public async Task ConfigureLogs()
    { 
        var embed = _embedService.GenerateEmbed()
            .WithTitle("Configurações de Logs")
            .WithDescription("Use os botões abaixo para configurar o modulo de logs")
            .Build();

        var controls = new ComponentBuilder()
            .WithButton("Canal de logs", "config_logs_channel_select")
            .Build();

        await RespondAsync(embed: embed, components: controls, ephemeral: true);
    }

    [SlashCommand("starboard", "Configura o modulo de starboard")]
    public async Task ConfigureStarboard()
    {
        var embed = _embedService.GenerateEmbed()
            .WithTitle("Configure Starboard")
            .WithDescription("Use os botões abaixo para configurar o modulo da Starboard")
            .Build();

        var controls = new ComponentBuilder()
            .WithButton("Canal da Starboard", "config_starboard_channel")
            .WithButton("Quantidade de estrelas", "config_starboard_min_stars")
            .Build();

        await RespondAsync(embed: embed, components: controls, ephemeral: true);
    }
}