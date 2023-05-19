using SharpEngine.Utils;
using WarriorSurvivor.Data;

namespace WarriorSurvivor.Manager;

public class SaveManager
{
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
        WS.PlayerData.Stats = Stats.FromSave(Save, "stats");
        WS.PlayerData.Stats.Level = 1;
        WS.PlayerData.Life = Save.GetObjectAs("life", WS.PlayerData.Life);
        WS.PlayerData.ActiveWeapon = WeaponData.FromSave(Save, "weapon");
        for (var i = 0; i < 5; i++)
            WS.PlayerData.PassiveWeapons[i] = WeaponData.FromSave(Save, $"weapon_{i}");
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
        
        WS.PlayerData.Stats.ToSave(Save, "stats");
        Save.SetObject("life", WS.PlayerData.Life);
        if (WS.PlayerData.ActiveWeapon == null)
            Save.SetObject("weapon", false);
        else
            WS.PlayerData.ActiveWeapon?.ToSave(Save, "weapon");
        for (var i = 0; i < 5; i++)
        {
            if(WS.PlayerData.PassiveWeapons[i] == null)
                Save.SetObject($"weapon_{i}", false);
            else
                WS.PlayerData.PassiveWeapons[i]?.ToSave(Save, $"weapon_{i}");
        }
        Save.Write("Resource/save.wssave");
    }
}