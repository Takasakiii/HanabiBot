using System.Text.Json.Serialization;

namespace Hanabi.Core.ViewModels;

public record GelbooruPostViewModel(
    [property:JsonPropertyName("title")] string Title,
    [property:JsonPropertyName("tags")] string Tags,
    [property:JsonPropertyName("file_url")] string FileUrl);