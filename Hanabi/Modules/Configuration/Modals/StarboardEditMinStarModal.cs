using Discord.Interactions;

namespace Hanabi.Modules.Configuration.Modals;

public class StarboardEditMinStarModal : IModal
{
    public string Title => "Defina a nova quantidade de estrelas";

    [InputLabel("Nova quantidade de estrelas:")]
    [ModalTextInput("starboard_edit_min_star_modal_new_stars", maxLength: 2, placeholder: "3")]
    public string NewStars { get; set; } = string.Empty;

}