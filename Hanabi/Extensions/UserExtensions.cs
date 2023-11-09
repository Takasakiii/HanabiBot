using Discord;

namespace Hanabi.Extensions;

public static class UserExtensions
{
    public static string GetSafeAvatarUrl(this IUser user, ushort size = 128)
    {
        return !string.IsNullOrWhiteSpace(user.AvatarId)
            ? user.GetAvatarUrl(size:size)
            : user.GetDefaultAvatarUrl();
    }
}