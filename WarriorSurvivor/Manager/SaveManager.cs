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
        WS.PlayerData.Gold = Save.GetObjectAs("gold", 0);
        WS.PlayerData.Reset();
        
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
        Save.SetObject("gold", WS.PlayerData.Gold);
        Save.Write("Resource/save.wssave");
    }
}