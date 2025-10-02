using ConsoleRpgEntities.Models;

public interface IBattleService
{
    BattleResult Battle(Player player, MonsterBase monster);
}
