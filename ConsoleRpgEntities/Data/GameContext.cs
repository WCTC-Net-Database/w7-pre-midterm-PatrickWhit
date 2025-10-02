using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleRpgEntities.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// GameContext is responsible for data storage and persistence.
/// It implements IContext and should only handle reading/writing data,
/// not business logic or cross-cutting concerns.
///
/// SRP: Only manages data access and persistence.
///
/// =======================
/// INSTRUCTIONAL COMMENTS
/// =======================
///
/// This class currently loads and saves game data (players, monsters, and items)
/// using JSON files. Items are loaded from a separate file (items.json), which
/// models how a relational database would use a separate table for items.
/// Players reference items by name (or ID), just as you would use foreign keys
/// in a database. After loading, we resolve these references to actual Item objects.
/// This approach makes it easy to transition to EF Core, where items would be a
/// DbSet<Item> and players would have navigation properties to their items.
/// </summary>
namespace ConsoleRpgEntities.Data
{
    public class GameContext : IContext
    {
        public List<Player> Players { get; set; }
        public List<MonsterBase> Monsters { get; set; }
        public List<Item> Items { get; set; }

        private readonly string _playersFile = "Files/players.json";
        private readonly string _monstersFile = "Files/monsters.json";
        private readonly string _itemsFile = "Files/items.json";

        public GameContext()
        {
            Players = new List<Player>();
            Monsters = new List<MonsterBase>();
            Items = new List<Item>();
            Read();
        }

        /// <summary>
        /// Loads players, monsters, and items from their respective JSON files into memory.
        /// For players, resolves item references to actual Item objects.
        /// </summary>
        public void Read()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Load Items from JSON (master list)
            if (File.Exists(_itemsFile))
            {
                var itemsJson = File.ReadAllText(_itemsFile);
                var loadedItems = JsonSerializer.Deserialize<List<Item>>(itemsJson, options);
                Items = loadedItems ?? new List<Item>();
            }
            else
            {
                Items = new List<Item>();
            }

            // Load Players from JSON
            // Players reference items by name (or ID) in the Items property (List<string>)
            if (File.Exists(_playersFile))
            {
                var playersJson = File.ReadAllText(_playersFile);

                // Temporary class for deserialization to get item names
                var tempPlayers = JsonSerializer.Deserialize<List<PlayerDTO>>(playersJson, options) ?? new List<PlayerDTO>();
                Players = new List<Player>();

                foreach (var temp in tempPlayers)
                {
                    var player = new Player
                    {
                        Name = temp.Name,
                        Profession = temp.Profession,
                        Level = temp.Level,
                        AbilityScores = temp.AbilityScores ?? new AbilityScores(),
                        Items = temp.Items != null
                            ? Items.Where(i => temp.Items.Any(name =>
                                    string.Equals(i.Name, name, System.StringComparison.OrdinalIgnoreCase)))
                                .ToList()
                            : new List<Item>()
                    };
                    Players.Add(player);
                }
            }
            else
            {
                Players = new List<Player>();
            }

            // Load Monsters from JSON (unchanged)
            if (File.Exists(_monstersFile))
            {
                var monstersJson = File.ReadAllText(_monstersFile);
                var monsterElements = JsonSerializer.Deserialize<List<JsonElement>>(monstersJson, options);
                Monsters = new List<MonsterBase>();

                if (monsterElements != null)
                {
                    foreach (var element in monsterElements)
                    {
                        if (element.TryGetProperty("Type", out var typeProp))
                        {
                            var type = typeProp.GetString();
                            MonsterBase? monster = null;
                            switch (type)
                            {
                                case "Goblin":
                                    monster = JsonSerializer.Deserialize<Goblin>(element.GetRawText(), options);
                                    break;
                                case "Dragon":
                                    monster = JsonSerializer.Deserialize<Dragon>(element.GetRawText(), options);
                                    break;
                            }
                            if (monster != null)
                                Monsters.Add(monster);
                        }
                    }
                }
            }
            else
            {
                Monsters = new List<MonsterBase>();
            }
        }

        /// <summary>
        /// Adds a player to the in-memory list.
        /// </summary>
        public void Write(Player player)
        {
            Players.Add(player);
        }

        /// <summary>
        /// Adds a monster to the in-memory list.
        /// </summary>
        public void Write(MonsterBase monster)
        {
            Monsters.Add(monster);
        }

        /// <summary>
        /// Adds an item to the in-memory list.
        /// </summary>
        public void Write(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Persists all players, monsters, and items to their respective JSON files.
        /// For players, only item names are saved (not full item objects).
        /// </summary>
        public int SaveChanges()
        {
            var playerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Save players as DTOs with item names only
            var playerDTOs = Players.Select(p => new PlayerDTO
            {
                Name = p.Name,
                Profession = p.Profession,
                Level = p.Level,
                AbilityScores = p.AbilityScores,
                Items = p.Items?.Select(i => i.Name).ToList()
            }).ToList();


            var playersJson = JsonSerializer.Serialize(playerDTOs, playerOptions);
            File.WriteAllText(_playersFile, playersJson);

            var monsterOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var monstersJson = JsonSerializer.Serialize(Monsters, monsterOptions);
            File.WriteAllText(_monstersFile, monstersJson);

            var itemOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var itemsJson = JsonSerializer.Serialize(Items, itemOptions);
            File.WriteAllText(_itemsFile, itemsJson);

            return Players.Count + Monsters.Count + Items.Count;
        }

        /// <summary>
        /// DTO for player serialization/deserialization.
        /// Stores item references as names (not full objects).
        /// </summary>
        private class PlayerDTO
        {
            public string Name { get; set; }
            public string Profession { get; set; }
            public int Level { get; set; }
            public AbilityScores AbilityScores { get; set; }
            public List<string> Items { get; set; }
        }

    }
}
