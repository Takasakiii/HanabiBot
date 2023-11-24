using Hanabi.Core.ViewModels;

namespace Hanabi.Core.Services.Interfaces;

public interface IImageService
{
    Task<GelbooruPostViewModel> GetRandomImage(string? tags = null);
}