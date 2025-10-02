using ConsoleRpgEntities.Models;

public class ConsoleGameUi : IGameUi
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Main Menu ===");
        Console.WriteLine("1. Level Up Player");
        Console.WriteLine("2. Add Player");
        Console.WriteLine("3. List All Players");
        Console.WriteLine("4. Battle a Monster");
        Console.WriteLine("5. Show Player Items"); // New option
        Console.WriteLine("0. Quit");
        Console.Write("Choose an action: ");
    }

    public string GetUserInput() => Console.ReadLine();

    public void ShowMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private string ReadNonEmptyString(string prompt, int minLength = 1, int maxLength = 30)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input) && input.Length >= minLength && input.Length <= maxLength)
                return input;
            Console.WriteLine($"Input must be {minLength}-{maxLength} characters and not empty.");
        }
    }

    private int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }

    public Player PromptForNewPlayer()
    {
        var name = ReadNonEmptyString("Enter player name: ", 2, 20);
        var profession = ReadNonEmptyString("Enter profession: ", 2, 20);

        int level = ReadIntInRange("Enter level (1-99): ", 1, 99);
        int health = ReadIntInRange("Enter max health (1-999): ", 1, 999);
        int hitPoints = ReadIntInRange("Enter current hit points (1-999): ", 1, health);

        int gold = ReadIntInRange("Enter starting gold (0-10000): ", 0, 10000);

        // Prompt for ability scores
        var abilityScores = new AbilityScores
        {
            Strength = ReadIntInRange("Strength (1-20): ", 1, 20),
            Dexterity = ReadIntInRange("Dexterity (1-20): ", 1, 20),
            Intelligence = ReadIntInRange("Intelligence (1-20): ", 1, 20),
            Wisdom = ReadIntInRange("Wisdom (1-20): ", 1, 20),
            Charisma = ReadIntInRange("Charisma (1-20): ", 1, 20),
            Constitution = ReadIntInRange("Constitution (1-20): ", 1, 20),
            Attack = ReadIntInRange("Attack (0-10): ", 0, 10),
            Defense = ReadIntInRange("Defense (0-10): ", 0, 10),
            Health = health,
            HitPoints = hitPoints,
            Gold = gold
        };

        // Prompt for item names (referencing items by name)
        Console.Write("Enter item names (comma or | separated, must match items.json): ");
        var itemsStr = Console.ReadLine();
        var itemNames = itemsStr?
            .Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Trim())
            .Where(e => !string.IsNullOrEmpty(e))
            .ToList() ?? new List<string>();

        // Create placeholder Item objects with only Name set
        var items = itemNames.Select(name => new Item { Name = name }).ToList();

        var player = new Player
        {
            Name = name,
            Profession = profession,
            Level = level,
            AbilityScores = abilityScores,
            Items = items // <-- This will serialize as item names in JSON
        };

        // Optionally, store item names in a property for later resolution
        // (if you add a List<string> ItemNames property to Player for this purpose)

        return player;
    }

    public int PromptForSelection(string prompt, int maxOption)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index > 0 && index <= maxOption)
                return index - 1;
            Console.WriteLine("Invalid selection. Please enter a valid number.");
        }
    }

    public void ShowPlayers(List<Player> players)
    {
        if (players == null || players.Count == 0)
        {
            Console.WriteLine("No players found.");
            return;
        }
        Console.WriteLine("\n=== All Players ===");
        for (int i = 0; i < players.Count; i++)
        {
            var p = players[i];
            Console.WriteLine($"{i + 1}. {p.Name,-18} | {p.Profession,-10} | Level: {p.Level,2} | HP: {p.AbilityScores.HitPoints,3} | Gold: {p.AbilityScores.Gold,5}");
            Console.WriteLine($"    Health: {p.AbilityScores.Health} | Items: {(p.Items != null && p.Items.Count > 0 ? string.Join(", ", p.Items.Select(item => item.Name)) : "None")}");
            Console.WriteLine($"    Ability Scores: " +
                $"Str:{p.AbilityScores.Strength} Dex:{p.AbilityScores.Dexterity} Int:{p.AbilityScores.Intelligence} Wis:{p.AbilityScores.Wisdom} " +
                $"Cha:{p.AbilityScores.Charisma} Con:{p.AbilityScores.Constitution} Atk:{p.AbilityScores.Attack} Def:{p.AbilityScores.Defense}");
        }
    }

    public void ShowMonsters(List<MonsterBase> monsters)
    {
        if (monsters == null || monsters.Count == 0)
        {
            Console.WriteLine("No monsters found.");
            return;
        }
        Console.WriteLine("\n=== Available Monsters ===");
        for (int i = 0; i < monsters.Count; i++)
        {
            var m = monsters[i];
            Console.WriteLine($"{i + 1}. {m.Name,-18} | {m.Type,-8} | Level: {m.Level,2} | HP: {m.Health,3}");
        }
    }

    public Player SelectPlayer(List<Player> players)
    {
        if (players == null || players.Count == 0)
        {
            ShowMessage("No players found. Please add a player first.");
            return null;
        }

        ShowPlayers(players);
        int index = PromptForSelection("Select a player by number: ", players.Count);
        return players[index];
    }

    public void ShowPlayerItems(Player player)
    {
        if (player == null)
        {
            ShowMessage("No player selected.");
            return;
        }
        if (player.Items == null || player.Items.Count == 0)
        {
            ShowMessage($"{player.Name} has no items.");
            return;
        }
        Console.WriteLine($"\n=== {player.Name}'s Items ===");
        foreach (var item in player.Items)
        {
            Console.WriteLine($"- {item.Name} ({item.Type})");
            Console.WriteLine($"  Description: {item.Description}");
            Console.WriteLine($"  Equipped: {(item.IsEquipped ? "Yes" : "No")}");
            if (item.AttributeModifiers != null && item.AttributeModifiers.Count > 0)
            {
                Console.WriteLine("  Modifiers:");
                foreach (var mod in item.AttributeModifiers)
                {
                    Console.WriteLine($"    {mod.Key}: {mod.Value}");
                }
            }
            else
            {
                Console.WriteLine("  Modifiers: None");
            }
            Console.WriteLine();
        }
    }
}
