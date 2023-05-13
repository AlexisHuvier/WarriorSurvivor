namespace WarriorSurvivor.Data;

public struct PlayerData
{
    public Stats Stats;
    public WeaponData? ActiveWeapon;
    public readonly WeaponData?[] PassiveWeapons;

    public PlayerData()
    {
        Stats = new Stats();
        ActiveWeapon = null;
        PassiveWeapons = new WeaponData?[] { null, null, null, null, null };
    }
}