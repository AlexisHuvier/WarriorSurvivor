using SharpEngine.Utils;

namespace WarriorSurvivor.Data;

public struct WeaponData
{
    public string Name;
    public Stats Stats;

    public WeaponData(string name, Stats stats)
    {
        Name = name;
        Stats = stats;
    }

    public void ToSave(Save save, string prefix)
    {
        save.SetObject($"{prefix}", true);
        save.SetObject($"{prefix}_name", Name);
        Stats.ToSave(save, $"{prefix}_stats");
    }
    public override string ToString() => $"[Name: {Name}, Stats: {Stats}]";
    
    public static WeaponData? FromSave(Save save, string prefix)
    {
        var exist = save.GetObjectAs($"{prefix}", false);
        if (!exist)
            return null;
        return new WeaponData
        {
            Name = save.GetObjectAs($"{prefix}_name", ""),
            Stats = Stats.FromSave(save, $"{prefix}_stats")
        };
    }
}