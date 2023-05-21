namespace WarriorSurvivor.Data;

public struct PlayerData
{
    public Stats Stats;
    public int Gold;
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

    public Stats GetPassiveStats()
    {
        var result = new Stats(0, 0, 0, 0);
        foreach (var passiveWeapon in PassiveWeapons)
        {
            if (!passiveWeapon.HasValue) continue;
            
            result.Life += passiveWeapon.Value.Stats.Life;
            result.Attack += passiveWeapon.Value.Stats.Attack;
            result.Speed += passiveWeapon.Value.Stats.Speed;
        }

        return result;
    }

    public bool ModifyGold(int diff)
    {
        if (Gold < diff)
            return false;
        Gold -= diff;
        return true;
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
        Life = Stats.Life + GetPassiveStats().Life;
        Stats.Level = 1;
        Exp = 0;
        ActiveWeapon = null;
        PassiveWeapons = new WeaponData?[] { null, null, null, null, null };
    }
}