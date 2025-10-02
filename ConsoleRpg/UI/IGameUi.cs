using ConsoleRpgEntities.Models;

public interface IGameUi
{
    void ShowMenu();
    string GetUserInput();
    void ShowMessage(string message);
    Player PromptForNewPlayer();
    int PromptForSelection(string prompt, int maxOption);
    void ShowPlayers(List<Player> players);
    void ShowMonsters(List<MonsterBase> monsters);
    Player SelectPlayer(List<Player> players);
    void ShowPlayerItems(Player player); // <-- Add this line
}
