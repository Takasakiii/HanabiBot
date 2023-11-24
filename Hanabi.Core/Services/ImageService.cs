using Hanabi.Core.Adapters.Interfaces;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Core.ViewModels;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Core.Services;

[Service<IImageService>]
public class ImageService(IGelbooruAdapter gelbooruAdapter, IHanabiConfig hanabiConfig) : IImageService
{
    public async Task<GelbooruPostViewModel> GetRandomImage(string? tags = null)
    {
        var endpoints = gelbooruAdapter.Endpoints;
        var result = await endpoints.GetPosts(
            hanabiConfig.GelbooruApiKey, 
            hanabiConfig.GelbooruUserId,
            limit: 1,
            tags: $"sort:random {tags}");

        return result.Post.First();
    }
}