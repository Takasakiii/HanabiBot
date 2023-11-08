using Discord;

namespace Hanabi.Services.Interfaces;

public interface IEmbedService
{
    EmbedBuilder GenerateEmbed();
    Embed GenerateSuccessEmbed(string message);
    Embed GenerateErrorEmbed(string message);
}