using ConsoleRpg.Decorators;
using ConsoleRpg.Helpers;
using ConsoleRpg.Services;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;

namespace ConsoleRpg;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Build configuration
        var configuration = ConfigurationHelper.GetConfiguration();

        // Create and bind FileLoggerOptions
        var fileLoggerOptions = new FileLoggerOptions();
        configuration.GetSection("Logging:File").Bind(fileLoggerOptions);

        // Configure logging
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));

            // Add Console logger
            loggingBuilder.AddConsole();

            // Add File logger using the correct constructor
            var logFileName = "Logs/log.txt"; // Specify the log file path

            loggingBuilder.AddProvider(new FileLoggerProvider(logFileName, fileLoggerOptions));
        });

        // Register your services
        services.AddSingleton<IContext, GameContext>();

        // Register generic DAOs
        services.AddSingleton<IEntityDao<Player>, PlayerDao>();
        services.AddSingleton<IEntityDao<MonsterBase>, MonsterDao>();
        services.AddSingleton<IGameUi, ConsoleGameUi>();
        services.AddSingleton<IBattleService, BattleService>();

        // Register PlayerService and the decorator for IPlayerService.
        // Now PlayerService uses IEntityDao<Player>.
        services.AddSingleton<PlayerService>();
        services.AddSingleton<IPlayerService>(provider =>
            new AutoSavePlayerServiceDecorator(
                provider.GetRequiredService<PlayerService>(),
                provider.GetRequiredService<IContext>()));

        services.AddSingleton<GameEngine>();
    }
}

