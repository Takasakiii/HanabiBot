using System.Text.Json.Serialization;

namespace Hanabi.Core.ViewModels;

public record GelbooruListResponseMetadata(
    [property:JsonPropertyName("limit")] int Limit,
    [property:JsonPropertyName("offset")] int Offset,
    [property:JsonPropertyName("count")] int Count);