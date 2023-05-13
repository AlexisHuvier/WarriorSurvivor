using SharpEngine.Utils;

namespace WarriorSurvivor.Data;

public struct WeaponData
{
    public string Name;
    public Stats Stats;
    public int Level = 1;

    public WeaponData(string name, Stats stats)
    {
        Name = name;
        Stats = stats;
    }

    public void ToSave(Save save, string prefix)
    {
        save.SetObject($"{prefix}", true);
        save.SetObject($"{prefix}_name", Name);
        save.SetObject($"{prefix}_level", Level);
        Stats.ToSave(save, $"{prefix}_stats");
    }
    public override string ToString() => $"[Name: {Name}, Level: {Level}, Stats: {Stats}]";
    
    public static WeaponData? FromSave(Save save, string prefix)
    {
        var exist = save.GetObjectAs($"{prefix}", false);
        if (!exist)
            return null;
        return new WeaponData
        {
            Name = save.GetObjectAs($"{prefix}_name", ""),
            Stats = Stats.FromSave(save, $"{prefix}_stats"),
            Level = save.GetObjectAs($"{prefix}_level", 1)
        };
    }
}