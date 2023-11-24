using System.Text.Json.Serialization;

namespace Hanabi.Core.ViewModels;

public record GelbooruListResponse<TType>(
    [property:JsonPropertyName("@attributes")] GelbooruListResponseMetadata Attributes,
    [property:JsonPropertyName("post")] IEnumerable<TType> Post);