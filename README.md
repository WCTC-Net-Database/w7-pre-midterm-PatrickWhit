### Prep Guide: Item-Driven Combat & Monster Enhancements

---

#### Overview:
Welcome to Week 7! This week, there will be no new assignments. Instead, you should focus on reviewing this Week 7 code and preparing for the upcoming in-class midterm exam. The exam will be heavily based on the concepts and code from class and Week 7, which is a culmination of the assignments that have come before. In the exam you will be asked to modify and extend a provided existing codebase.

## What to *Potentially* Expect in the Exam

During the in-class midterm exam, you will be asked to add new functionality to the existing codebase. Here are **some examples of the types of modifications you <u>might</u> be asked to make**:

### 1. Add a New Monster
You might be required to introduce a new monster to the game. This will involve:
- Creating a new monster class that inherits from an existing abstract class or implements an interface for behavior.
- Adding the new monster to the game's logic and adding it to a room, making sure it is visible when the player visits the room.

### 2. Enhance Attack Functionality
You could be asked to improve the attack functionality. This might include:
- Adding new attack types or special abilities.
- Modifying the existing attack logic to include new features such as reducing hit points based on damage, or allowing for multiple hits.

### 3. Update Equipment List After Combat
You may need to update the equipment list after combat. This will involve:
- Modifying the combat logic to include equipment drops.
- Updating the player's inventory with new items.

### 4. Improve the Menu
You might be asked to enhance the game's menu. This could include:
- Adding new menu options.
- Improving the user interface and navigation.

## How to Prepare

To prepare for the exam, you should thoroughly review the Week 7 code. Focus on understanding the following concepts:

### Data Context
- Understand how the existing data context is reading in the `monsters.json` (for example) and adding characters (ghost, goblin) to the game.

### Abstract Classes and Interfaces
- Review how abstract classes and interfaces are used to define common behaviors and properties.
- Understand how to implement new classes that inherit from abstract classes or implement interfaces.

### SOLID Principles
- Understand how the SOLID Principles are applied in the codebase.
- For example, review how high-level modules depend on abstractions rather than concrete implementations.

### Code Modularity and Extensibility
- Understand how the code is structured to promote modularity and extensibility.
- Review how new functionality can be added without modifying existing code.

## Tips for Success

- **Practice:** Implement your own modifications to the Week 7 code to get comfortable with adding new functionality in preparation for the midterm.
- **Understand the Concepts:** Make sure you understand the key concepts of factory methods, abstract classes, interfaces, etc, as mentioned above.
- **Ask Questions:** If you have any questions or need clarification, don't hesitate to reach out for help.

---

#### Example Assignment:

In this assignment, you will extend your RPG combat system to use items for both players and (optionally) monsters. 
You will update your combat logic so that equipped items (from `items.json`) affect attack, defense, and other stats during battles.

#### Learning Objectives:
- Use item attribute modifiers to affect combat outcomes.
- Apply the Open/Closed Principle by extending combat logic without modifying core entity classes.
- Practice using LINQ and object relationships in C#.

---

### Assignment Tasks:

#### 1. Player Item-Driven Combat (Required)
- Update `Player.cs` and `BattleService.cs` so that player items (from `items.json`) affect combat.
  - Equipped weapons should increase attack damage.
  - Equipped armor should reduce incoming damage.
  
  **Example: Player Attack Calculation**
  ```csharp
  int baseAttack = 5 + player.Level; 
  int itemAttackBonus = player.Items
      .Where(item => item.IsEquipped &&
             item.AttributeModifiers.ContainsKey(Attribute.Attack))
      .Sum(item => item.AttributeModifiers[Attribute.Attack]); 
  int totalAttack = baseAttack + itemAttackBonus;
  ```
  
  **Example: Player Defense Calculation**
  
  ```csharp
  int baseDefense = player.AbilityScores.Defense; 
  int itemDefenseBonus = player.Items
      .Where(item => item.IsEquipped && 
             item.AttributeModifiers.ContainsKey(Attribute.Defense))
      .Sum(item => item.AttributeModifiers[Attribute.Defense]); 
  int totalDefense = baseDefense + itemDefenseBonus;
  ```
  
  **Primary Task Completion Tips:**
  
    * Modify only `BattleService.cs` for the required functionality.
    * Use the provided `TODO` comments and examples to:
        * Calculate total attack and defense for the player using equipped items.
        * Use LINQ to sum up item bonuses.
        * Apply these calculations in the battle loop.
  
    **Why This Is Sufficient**
    * The rest of the codebase (models, item loading, player creation) is already set up to provide the necessary data.
    * The `Player` class exposes `Items` and `AbilityScores`, so you can use these directly.
    * The assignment and comments in `BattleService.cs` are clear, step-by-step, and reference the rubric and examples in README.md.
  
#### 2. Monster Item-Driven Combat (Stretch Goal(s))
- Extend `MonsterBase.cs` and `monsters.json` so monsters can also use items in combat.
  - Implement logic in `BattleService.cs` to factor monster items into their attacks and defenses.
    - Note this is very similar to what you did above so this should be a copy/paste exercise

- Potions or consumables may restore health or provide other effects.

  - This will require additional work outside of what is provided

  **Example: Monster Attack/Defense Calculation**

  ```csharp
  int monsterAttackBonus = monster.Items?
      .Where(item => item.IsEquipped && 
             item.AttributeModifiers.ContainsKey(Attribute.Attack))
      .Sum(item => item.AttributeModifiers[Attribute.Attack]) ?? 0; 
  int monsterDefenseBonus = monster.Items?
      .Where(item => item.IsEquipped && 
             item.AttributeModifiers.ContainsKey(Attribute.Defense))
      .Sum(item => item.AttributeModifiers[Attribute.Defense]) ?? 0;
  ```

    **Stretch Goal Completion Tips:**
    * Stretch Goal: If you wish to attempt the monster item-driven combat, you may need to extend the MonsterBase class and update how monsters are loaded and equipped with items.
    * Consumable Logic: If you wish to implement potion/consumable effects, you may need to add logic elsewhere (e.g., in the UI or player model).

#### 3. Inline Guidance
- Follow the inline comments and TODOs in the codebase for specific instructions.

---

### Rubric

| Category                          | Full Marks (100)              | Partial Marks (50-90)                   | Minimal Marks (10-40)                     | No Marks (0)                           |
|-----------------------------------|------------------------------|---------------------------------------|-----------------------------------------|----------------------------------------|
| **Player Item Combat**            | Correctly implements item-driven combat for players. | Implements with some logic issues or missing features. | Attempts but with significant errors. | No attempt or non-functional. |
| **Monster Item Combat (Stretch+10%)** | Correctly implements item-driven combat for monsters. | Implements with some logic issues or missing features. | Attempts but with significant errors. | No attempt or non-functional. |
| **Consumable Item (Stretch+20%)** | Correctly implements consumables for players or monsters. | Implements with some logic issues or missing features. | Attempts but with significant errors. | No attempt or non-functional. |
| **Code Quality & Testing**        | Code is clean, well-tested, and easy to follow. | Minor issues in code quality or testing. | Major issues in code quality or testing. | Code is untestable or unreadable. |

---

### Additional Resources:
- [Working with JSON in C#](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)
- [LINQ in C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [Open/Closed Principle (OCP) Explanation](https://stackify.com/solid-design-open-closed-principle/)

---

## 💡 Tips

- Stick to **small commits** with meaningful messages.
- Use the `ConsoleRpgEntities` project for logic and the `ConsoleRpg` for interaction.
- Test early and often.
- Use interfaces to keep your design open to expansion.

Good luck, and build something epic!

