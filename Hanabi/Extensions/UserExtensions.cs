using Discord;

namespace Hanabi.Extensions;

public static class UserExtensions
{
    public static string GetSafeAvatarUrl(this IUser user)
    {
        return !string.IsNullOrWhiteSpace(user.AvatarId)
            ? user.GetAvatarUrl()
            : user.GetDefaultAvatarUrl();
    }
}