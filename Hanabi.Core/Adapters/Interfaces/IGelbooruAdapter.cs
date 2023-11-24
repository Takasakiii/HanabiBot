using Hanabi.Core.Adapters.Endpoints;

namespace Hanabi.Core.Adapters.Interfaces;

public interface IGelbooruAdapter
{
    IGelbooruEndpoints Endpoints { get; }
}