using Discord;
using Discord.Interactions;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using Hanabi.Modules.Configuration.Modals;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Configuration;

public class ModalsResponse : InteractionModuleBase
{
    private readonly IEmbedService _embedService;
    private readonly IServerConfigurationService _serverConfigurationService;

    public ModalsResponse(IEmbedService embedService, IServerConfigurationService serverConfigurationService)
    {
        _embedService = embedService;
        _serverConfigurationService = serverConfigurationService;
    }
    
    [ModalInteraction("config_starboard_min_stars_edit_modal")]
    public async Task ConfigStarboardMinStarsEditModal(StarboardEditMinStarModal modal)
    {
        if (!uint.TryParse(modal.NewStars, out var qtStars))
        {
            var errEmbed = _embedService.GenerateErrorEmbed("Quantidade invalida");
            await RespondAsync(embed: errEmbed, ephemeral: true);
            return;
        }
        
        var config = await _serverConfigurationService.GetServerConfig(Context.Guild.Id) ??
                     new ServerConfigurationViewModel(Context.Guild.Id);

        config.StarBoardMinimalStars = qtStars;
            
        await _serverConfigurationService.EditConfig(config);

        var embed = _embedService.GenerateSuccessEmbed("Configuração das estrelas foi salva com sucesso");
        await RespondAsync(embed: embed, ephemeral: true);
    }
}