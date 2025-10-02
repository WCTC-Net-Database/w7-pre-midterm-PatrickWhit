// The code below is commented out for now, but will be used in a future lesson.
// It demonstrates Dependency Injection (DI) using Microsoft's built-in DI container.
// DI is a key part of the "D" in SOLID (Dependency Inversion Principle).
// With DI, we let a container create and provide our dependencies, making our code more flexible and testable.
// For now, we are using manual instantiation (see below), but soon you'll see how DI can simplify and decouple this process.


using ConsoleRpg.Decorators;
using ConsoleRpg.Helpers;
using ConsoleRpg.Services;
using ConsoleRpgEntities.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRpg;

public static class Program
{
    private static void Main(string[] args)
    {
        // Setup DI container
        var serviceCollection = new ServiceCollection();
        Startup.ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get the GameEngine and run it
        var gameEngine = serviceProvider.GetService<GameEngine>();
        gameEngine?.Run();
    }
}
