namespace WarriorSurvivor.Data;

public class EnemyData
{
    public Stats Stats;
    public int Life;
    public string Sprite;

    public EnemyData()
    {
        Stats = new Stats();
        Life = Stats.Life;
        Sprite = "";
    }
}