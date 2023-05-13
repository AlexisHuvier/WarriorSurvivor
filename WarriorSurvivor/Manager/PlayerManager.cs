using SharpEngine.Utils;
using WarriorSurvivor.Data;

namespace WarriorSurvivor.Manager;

public class PlayerManager
{
    public PlayerData PlayerData = new();
    public Save? Save;

    public void Init()
    {
        Save = Save.Load("Resource/save.wssave", new Dictionary<string, object>
        {
            { "stats_life", 20 },
            { "stats_speed", Rand.GetRand(300, 350) },
            { "stats_attack", Rand.GetRand(0, 3) },
            { "stats_defense", Rand.GetRand(0, 3) }
        });
        PlayerData.Stats = Stats.FromSave(Save, "stats");
        PlayerData.ActiveWeapon = WeaponData.FromSave(Save, "weapon");
        for (var i = 0; i < 5; i++)
            PlayerData.PassiveWeapons[i] = WeaponData.FromSave(Save, $"weapon_{i}");
    }

    public void Reset()
    {
        if(File.Exists("Resource/save.wssave"))
            File.Delete("Resource/save.wssave");
        Init();
    }

    public void WriteSave()
    {
        if(Save == null)
            return;
        
        PlayerData.Stats.ToSave(Save, "stats");
        if (PlayerData.ActiveWeapon == null)
            Save.SetObject("weapon", false);
        else
            PlayerData.ActiveWeapon?.ToSave(Save, "weapon");
        for (var i = 0; i < 5; i++)
        {
            if(PlayerData.PassiveWeapons[i] == null)
                Save.SetObject($"weapon_{i}", false);
            else
                PlayerData.PassiveWeapons[i]?.ToSave(Save, $"weapon_{i}");
        }
        Save.Write("Resource/save.wssave");
    }
}