namespace Hanabi.Extensions;

public static class StringExtensions
{
    public static string CutTheEnd(this string content, int maxLenght = 2048)
    {
        if (content.Length <= 2048) return content;
        var subContent = content[..(maxLenght - 3)];
        return $"{subContent}...";

    }
}