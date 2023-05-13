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

    public override string ToString() => $"[Name: {Name}, Level: {Level}, Stats: {Stats}]";
}