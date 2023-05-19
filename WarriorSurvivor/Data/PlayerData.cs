namespace WarriorSurvivor.Data;

public struct PlayerData
{
    public Stats Stats;
    public int Life;
    public int Exp = 0;
    public WeaponData? ActiveWeapon;
    public WeaponData?[] PassiveWeapons;

    public PlayerData()
    {
        PassiveWeapons = new WeaponData?[5];
        
        Stats = new Stats();
        Reset();
    }

    public bool AddExp(int exp)
    {
        Exp += exp;
        if (Exp >= 1 + Stats.Level * 2)
        {
            Exp -= 1 + Stats.Level * 2;
            Stats.Level++;
            return true;
        }
        return false;
    }

    public int GetExpToNextLevel() => 1 + Stats.Level * 2;

    public void Reset()
    {
        Life = Stats.Life;
        Stats.Level = 1;
        Exp = 0;
        ActiveWeapon = null;
        PassiveWeapons = new WeaponData?[] { null, null, null, null, null };
    }
}