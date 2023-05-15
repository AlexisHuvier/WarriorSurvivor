namespace WarriorSurvivor.Data;

public struct PlayerData
{
    public Stats Stats;
    public int Life;
    public WeaponData? ActiveWeapon;
    public WeaponData?[] PassiveWeapons;

    public PlayerData()
    {
        PassiveWeapons = new WeaponData?[5];
        
        Stats = new Stats();
        Reset();
    }

    public void Reset()
    {
        Life = Stats.Life;
        ActiveWeapon = null;
        PassiveWeapons = new WeaponData?[] { null, null, null, null, null };
    }
}