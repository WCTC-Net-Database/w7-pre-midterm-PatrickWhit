using ConsoleRpg.Services;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models;

namespace ConsoleRpg;

public class GameEngine
{
    private readonly IPlayerService _playerService;
    private Player _player;
    private readonly IEntityDao<MonsterBase> _monsterDao;
    private readonly IGameUi _ui;
    private readonly IBattleService _battleService;

    public GameEngine(
        IPlayerService playerService,
        IEntityDao<MonsterBase> monsterDao,
        IGameUi ui,
        IBattleService battleService)
    {
        _playerService = playerService;
        _monsterDao = monsterDao;
        _ui = ui;
        _battleService = battleService;
    }

    public void Run()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        var players = _playerService.GetAllPlayers();
        _player = _ui.SelectPlayer(players);

        if (_player == null)
        {
            _ui.ShowMessage("No player selected. Exiting game setup.");
            Environment.Exit(0);
        }
        _ui.ShowMessage($"{_player.Name} has entered the game.");
        Thread.Sleep(500);
        GameLoop();
    }

    private void GameLoop()
    {
        while (true)
        {
            _ui.ShowMenu();
            var input = _ui.GetUserInput();

            switch (input)
            {
                case "1":
                    _ui.ShowMessage("Leveling up player...");
                    _playerService.LevelUpPlayer(_player);
                    break;
                case "2":
                    var newPlayer = _ui.PromptForNewPlayer();
                    if (newPlayer != null)
                    {
                        _playerService.AddPlayer(newPlayer);
                    }
                    break;
                case "3":
                    var players = _playerService.GetAllPlayers();
                    _ui.ShowPlayers(players);
                    _ui.ShowMessage("Press Enter to return to the menu...");
                    _ui.GetUserInput();
                    break;
                case "4":
                    BattleMonster();
                    break;
                case "5":
                    _ui.ShowPlayerItems(_player);
                    _ui.ShowMessage("Press Enter to return to the menu...");
                    _ui.GetUserInput();
                    break;
                case "0":
                    _ui.ShowMessage("Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    _ui.ShowMessage("Invalid selection. Please choose 1, 2, 3, 4, 5, or 0.");
                    break;
            }
        }
    }

    private void BattleMonster()
    {
        var monsters = _monsterDao.GetAll();
        if (monsters == null || monsters.Count == 0)
        {
            _ui.ShowMessage("No monsters found.");
            return;
        }

        _ui.ShowMonsters(monsters);
        int index = _ui.PromptForSelection("Select a monster by number: ", monsters.Count);
        var monster = monsters[index];
        var battleResult = _battleService.Battle(_player, monster);

        foreach (var evt in battleResult.Events)
            _ui.ShowMessage(evt);
    }
}
