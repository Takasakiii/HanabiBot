using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using Hanabi.Modules.Configuration.Modals;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Configuration.Interactions;

public class StarboardConfigInteractions : InteractionModuleBase<SocketInteractionContext<SocketMessageComponent>>
{
    private readonly IEmbedService _embedService;
    private readonly IServerConfigurationService _serverConfigurationService;

    public StarboardConfigInteractions(IEmbedService embedService, IServerConfigurationService serverConfigurationService)
    {
        _embedService = embedService;
        _serverConfigurationService = serverConfigurationService;
    }

    [ComponentInteraction("config_starboard_channel")]
    public async Task ConfigStarboardChannel()
    {
        var currentConfig = await _serverConfigurationService.GetServerConfig(Context.Guild.Id);

        var embed = _embedService.GenerateEmbed()
            .WithTitle("Editar canal da Starboard")
            .WithDescription("Selecione o novo canal para enviar as perolas");

        if (currentConfig?.StarBoardChannel is not null)
        {
            embed = embed.AddField("Canal atualmente selecionado:", $"<#{currentConfig.StarBoardChannel}>");
        }

        var controls = new ComponentBuilder()
            .WithSelectMenu(
                "config_starboard_channel_select",
                placeholder: "Selecione novo canal",
                type: ComponentType.ChannelSelect,
                channelTypes: new[]
                {
                    ChannelType.Text
                })
            .Build();

        await RespondAsync(embed: embed.Build(), components: controls, ephemeral: true);
    }
    
    [ComponentInteraction("config_starboard_channel_select")]
    public async Task EditStarboardChannelSelect(IChannel[] selectedChannels)
    {
        var channel = selectedChannels[0];
        var config = await _serverConfigurationService.GetServerConfig(Context.Guild.Id) ??
                     new ServerConfigurationViewModel(Context.Guild.Id);

        config.StarBoardChannel = channel.Id;
            
        await _serverConfigurationService.EditConfig(config);

        var embed = _embedService.GenerateSuccessEmbed("Configuração do canal da starboard salva com sucesso");
        await RespondAsync(embed: embed, ephemeral: true);
    }
    
    [ComponentInteraction("config_starboard_min_stars")]
    public async Task ConfigStarboardMinStar()
    {
        var currentConfig = await _serverConfigurationService.GetServerConfig(Context.Guild.Id);

        var embed = _embedService.GenerateEmbed()
            .WithTitle("Editar estrelas necessarias para starboard")
            .WithDescription("Defina a nova quantidade de estrelas necessarias para enviar uma perola");

        if (currentConfig?.StarBoardMinimalStars is not null)
        {
            embed = embed.AddField("Quantidade atualmente selecionada:", currentConfig.StarBoardMinimalStars);
        }

        var controls = new ComponentBuilder()
            .WithButton("Editar", "config_starboard_min_stars_edit")
            .Build();

        await RespondAsync(embed: embed.Build(), components: controls, ephemeral: true);
    }

    [ComponentInteraction("config_starboard_min_stars_edit")]
    public async Task ConfigStarboardMinStarOpenModal() =>
        await RespondWithModalAsync<StarboardEditMinStarModal>("config_starboard_min_stars_edit_modal");
}