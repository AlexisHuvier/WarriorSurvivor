namespace WarriorSurvivor.Data.DB;

public class Weapon
{
    public readonly string Name;
    public readonly string Icon;
    public readonly string Description;
    public readonly Stats BaseStats;
    public readonly string ClassName;
    public readonly bool IsActive;

    public Weapon(string name, string icon, string description, Stats baseStats, string className, bool isActive)
    {
        Name = name;
        Icon = icon;
        Description = description;
        BaseStats = baseStats;
        ClassName = className;
        IsActive = isActive;
    }

    public static readonly Dictionary<string, Weapon> Types = new()
    {
        { "Bottes Ailées", new Weapon("Bottes Ailées", "weapon-bottes_ailees", "Bottes ailées qui permettent d'aller plus vite", new Stats(1, 0, 50, 0), "", false) },
        { "Cristal de Vie", new Weapon("Cristal de Vie", "weapon-cristal_vie", "Cristal rouge ajoutant un peu de vie", new Stats(1, 2, 0, 0), "", false) },
        { "Haltère", new Weapon("Haltère", "weapon-haltere", "Haltère sportive ajoutant de l'attaque", new Stats(1, 0, 0, 1), "", false) }
    };
}