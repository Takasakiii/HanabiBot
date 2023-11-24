using Hanabi.Core.ViewModels;
using Refit;

namespace Hanabi.Core.Adapters.Endpoints;

public interface IGelbooruEndpoints
{
    [Get("/index.php?page=dapi&s=post&q=index&json=1")]
    Task<GelbooruListResponse<GelbooruPostViewModel>> GetPosts(
        [Query, AliasAs("api_key")] string apiKey, 
        [Query, AliasAs("user_id")] string userId, 
        [Query] uint? limit = null, 
        [Query] uint? pid = null,
        [Query] string? tags = null, 
        [Query] string? cid = null, 
        [Query] string? id = null);
}