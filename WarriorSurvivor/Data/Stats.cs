using SharpEngine.Utils;

namespace WarriorSurvivor.Data;

public struct Stats
{

    public int Life;
    public int Speed;
    public int Attack;

    public void ToSave(Save save, string prefix)
    {
        save.SetObject($"{prefix}_life", Life);
        save.SetObject($"{prefix}_speed", Speed);
        save.SetObject($"{prefix}_attack", Attack);
    }
    
    public override string ToString() => $"[Life: {Life}, Speed: {Speed}, Attack: {Attack}]";
    
    public static Stats FromSave(Save save, string prefix)
    {
        return new Stats
        {
            Life = save.GetObjectAs($"{prefix}_life", 20),
            Speed = save.GetObjectAs($"{prefix}_speed", 300),
            Attack = save.GetObjectAs($"{prefix}_attack", 0)
        };
    }
}