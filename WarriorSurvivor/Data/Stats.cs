using SharpEngine.Utils;

namespace WarriorSurvivor.Data;

public struct Stats
{
    public int Level;
    public int Life;
    public int Speed;
    public int Attack;

    public Stats(int level, int life, int speed, int attack)
    {
        Level = level;
        Life = life;
        Speed = speed;
        Attack = attack;
    }

    public void ToSave(Save save, string prefix)
    {
        save.SetObject($"{prefix}_level", Level);
        save.SetObject($"{prefix}_life", Life);
        save.SetObject($"{prefix}_speed", Speed);
        save.SetObject($"{prefix}_attack", Attack);
    }
    
    public override string ToString() => $"[Level: {Level}, Life: {Life}, Speed: {Speed}, Attack: {Attack}]";
    
    public static Stats FromSave(Save save, string prefix)
    {
        return new Stats
        {
            Level = save.GetObjectAs($"{prefix}_level", 1),
            Life = save.GetObjectAs($"{prefix}_life", 20),
            Speed = save.GetObjectAs($"{prefix}_speed", 300),
            Attack = save.GetObjectAs($"{prefix}_attack", 0)
        };
    }
}