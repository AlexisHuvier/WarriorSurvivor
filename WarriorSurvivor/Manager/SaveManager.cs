using SharpEngine.Utils;
using WarriorSurvivor.Data;
using WarriorSurvivor.Entity.PassiveWeapon;

namespace WarriorSurvivor.Manager;

public class SaveManager
{
    private Save? _save;

    public void Init()
    {
        _save = Save.Load("Resource/save.wssave", new Dictionary<string, object>
        {
            { "stats_life", 20 },
            { "stats_speed", Rand.GetRand(300, 350) },
            { "stats_attack", Rand.GetRand(0, 3) },
            { "stats_defense", Rand.GetRand(0, 3) }
        });
        WS.PlayerData.Stats = Stats.FromSave(_save, "stats");
        WS.PlayerData.Stats.Level = 1;
        WS.PlayerData.Gold = _save.GetObjectAs("gold", 0);
        for (var i = 0; i < 3; i++)
            WS.PlayerData.UpgradeBuy[i] = _save.GetObjectAs($"buy_{i}", 0);
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
        if(_save == null)
            return;
        
        WS.PlayerData.Stats.ToSave(_save, "stats");
        _save.SetObject("gold", WS.PlayerData.Gold);
        for(var i = 0; i < 3; i++)
            _save.SetObject($"buy_{i}", WS.PlayerData.UpgradeBuy[i]);
        _save.Write("Resource/save.wssave");
    }
}