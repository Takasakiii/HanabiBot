using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Hanabi.Core;
using Hanabi.Core.Services.Interfaces;
using Hanabi.Services.Interfaces;
using Lina.AutoDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var discordClientConfig = new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.All,
    MessageCacheSize = 200
};

var discordClient = new DiscordSocketClient(discordClientConfig);
var interactionService = new InteractionService(discordClient.Rest);

var diSetup = new ServiceCollection();
diSetup.AddSingleton(discordClient);
diSetup.AddSingleton(interactionService);
diSetup.AddAutoDependencyInjection<Program>();
diSetup.AddHanabiCore();

var di = diSetup.BuildServiceProvider();

await interactionService.AddModulesAsync(typeof(Program).Assembly, di);
var discordToken = di.GetRequiredService<IHanabiConfig>().DiscordBotToken;
di.GetRequiredService<IAutoLoadEventsService>().Initialize();
await di.MigrateDatabase();
await discordClient.LoginAsync(TokenType.Bot, discordToken);
await discordClient.StartAsync();

await Task.Delay(Timeout.Infinite);