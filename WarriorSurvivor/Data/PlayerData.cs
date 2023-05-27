using SharpEngine.Utils;

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

    public int GetNumberNullPassiveWeapon() => PassiveWeapons.Count(weapon => weapon == null);

    public KeyValuePair<int, WeaponData> GetRandomNotNullPassiveWeapon()
    {
        var passiveWeapons = PassiveWeapons.Where(x => x.HasValue).ToList();
        WeaponData? result = null;
        var index = 0;
        while (result == null)
        {
            index = Rand.GetRand(0, passiveWeapons.Count);
            result = passiveWeapons[index];
        }

        return new KeyValuePair<int, WeaponData>(index, result.Value);
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
        SoundManager.Play("gold");
        return true;
    }

    public bool AddExp(int exp)
    {
        Exp += exp;
        
        if (Exp < 1 + Stats.Level * 2) return false;
        
        Exp -= 1 + Stats.Level * 2;
        Stats.Level++;
        if (Exp >= 1 + Stats.Level * 2)
            Exp = Stats.Level * 2;
        
        return true;
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