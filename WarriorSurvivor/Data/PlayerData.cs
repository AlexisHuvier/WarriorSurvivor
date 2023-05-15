namespace WarriorSurvivor.Data;

public struct PlayerData
{
    public Stats Stats;
    public int Life;
    public WeaponData? ActiveWeapon;
    public readonly WeaponData?[] PassiveWeapons;

    public PlayerData()
    {
        Stats = new Stats();
        Life = Stats.Life;
        ActiveWeapon = null;
        PassiveWeapons = new WeaponData?[] { null, null, null, null, null };
    }
}