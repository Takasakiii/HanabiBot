using Hanabi.Core.Adapters.Endpoints;
using Hanabi.Core.Adapters.Interfaces;
using Hanabi.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Refit;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

namespace Hanabi.Core.Adapters;


[HttpClient<IGelbooruAdapter>]
public class GelbooruAdapter : IGelbooruAdapter
{
    public GelbooruAdapter(HttpClient client, IConfiguration configuration)
    {
        client.BaseAddress =
            new Uri(configuration["Urls:Gelbooru"] ?? throw new StartupException("Gelbooru url is undefined"));
        Endpoints = RestService.For<IGelbooruEndpoints>(client);
    }

    public IGelbooruEndpoints Endpoints { get; }
}