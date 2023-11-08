using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using Hanabi.Services.Interfaces;

namespace Hanabi.Modules.Configuration.Interactions;

public class LogsConfigInteractions : InteractionModuleBase<SocketInteractionContext<SocketMessageComponent>>
{
    private readonly IEmbedService _embedService;
    private readonly IServerConfigurationService _serverConfigurationService;

    public LogsConfigInteractions(IEmbedService embedService, IServerConfigurationService serverConfigurationService)
    {
        _embedService = embedService;
        _serverConfigurationService = serverConfigurationService;
    }

    [ComponentInteraction("config_logs_channel_select")]
    public async Task EditLogChannel()
    {
        var currentConfig = await _serverConfigurationService.GetServerConfig(Context.Guild.Id);

        var embed = _embedService.GenerateEmbed()
            .WithTitle("Editar canal de logs")
            .WithDescription("Selecione o novo canal para enviar os logs");

        if (currentConfig?.LogsChatId is not null)
        {
            embed = embed.AddField("Canal atualmente selecionado:", $"<#{currentConfig.LogsChatId}>");
        }

        var controls = new ComponentBuilder()
            .WithSelectMenu(
                "config_logs_channel_select_select",
                placeholder: "Selecione novo canal",
                type: ComponentType.ChannelSelect,
                channelTypes: new[]
                {
                    ChannelType.Text
                })
            .Build();

        await RespondAsync(embed: embed.Build(), components: controls, ephemeral: true);
    }

    [ComponentInteraction("config_logs_channel_select_select")]
    public async Task EditLogChannelSelect(IChannel[] selectedChannels)
    {
        var channel = selectedChannels[0];
        var config = await _serverConfigurationService.GetServerConfig(Context.Guild.Id) ??
                     new ServerConfigurationViewModel(Context.Guild.Id);

        config.LogsChatId = channel.Id;
            
        await _serverConfigurationService.EditConfig(config);

        var embed = _embedService.GenerateSuccessEmbed("Configuração do canal de logs salva com sucesso");
        await RespondAsync(embed: embed, ephemeral: true);
    }
}