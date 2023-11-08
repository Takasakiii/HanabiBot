using FluentValidation;

namespace Hanabi.Core.Models.Validations;

public class ServerConfigurationValidator : AbstractValidator<ServerConfiguration>
{
    public ServerConfigurationValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0UL);
        RuleFor(x => x.LogsChatId).GreaterThan(0UL);
    }
}