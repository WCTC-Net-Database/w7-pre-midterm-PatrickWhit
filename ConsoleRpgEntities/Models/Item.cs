using System.Collections.Generic;
using System.Linq;

namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents an item that can affect player attributes.
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // e.g., "Weapon", "Armor", "Potion"
        public bool IsEquipped { get; set; }
        public Dictionary<Attribute, int> AttributeModifiers { get; set; } = new();

        public Item() { }

        public Item(string name, string type, Dictionary<Attribute, int> attributeModifiers, string description = "", bool isEquipped = false)
        {
            Name = name;
            Type = type;
            AttributeModifiers = attributeModifiers ?? new Dictionary<Attribute, int>();
            Description = description;
            IsEquipped = isEquipped;
        }

        public override string ToString()
        {
            var mods = AttributeModifiers != null && AttributeModifiers.Count > 0
                ? string.Join(", ", AttributeModifiers.Select(kv => $"{kv.Key}: {kv.Value}"))
                : "None";
            return $"{Name} (Type: {Type}, Equipped: {IsEquipped}) | Modifiers: {mods}";
        }

        // TODO: When calculating player or monster stats in combat, only include items
        //       that are equipped (IsEquipped == true) and have relevant AttributeModifiers.
        //       For example, weapons should provide Attack bonuses, armor should provide Defense bonuses,
        //       and potions may provide healing or other effects.
    }
}
