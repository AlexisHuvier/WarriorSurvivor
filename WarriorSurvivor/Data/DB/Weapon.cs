using WarriorSurvivor.Entity.Weapon;

namespace WarriorSurvivor.Data.DB;

public class Weapon
{
    public readonly string Name;
    public readonly string Icon;
    public readonly string Description;
    public readonly Stats BaseStats;
    public readonly string ClassName;
    public readonly bool IsActive;

    private Weapon(string name, string icon, string description, Stats baseStats, string className, bool isActive)
    {
        Name = name;
        Icon = icon;
        Description = description;
        BaseStats = baseStats;
        ClassName = className;
        IsActive = isActive;
    }

    public Type? GetEntity()
    {
        return ClassName switch
        {
            "FireCircle" => typeof(FireCircle),
            "KnifeSpawner" => typeof(KnifeSpawner),
            _ => null
        };
    }

    public static readonly Dictionary<string, Weapon> Types = new()
    {
        { "Bottes Ailées", new Weapon("Bottes Ailées", "weapon-bottes_ailees", "Bottes ailées qui permettent d'aller plus vite", new Stats(1, 0, 50, 0), "", false) },
        { "Cristal de Vie", new Weapon("Cristal de Vie", "weapon-cristal_vie", "Cristal rouge ajoutant un peu de vie", new Stats(1, 2, 0, 0), "", false) },
        { "Haltère", new Weapon("Haltère", "weapon-haltere", "Haltère sportive ajoutant de l'attaque", new Stats(1, 0, 0, 1), "", false) },
        { "Cercle de Feu", new Weapon("Cercle de Feu", "weapon-cercle_feu", "Boules de feu formant un cercle", new Stats(1, 0, 0, 0), "FireCircle", false) },
        { "Couteau de Lancer", new Weapon("Couteau de Lancer", "weapon-couteau", "Couteau qui se lance vers l'ennemi le plus proche", new Stats(1, 0, 0, 0), "KnifeSpawner", false)}
    };
}