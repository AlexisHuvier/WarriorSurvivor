using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Widget;

public class PlayerDisplayer: SharpEngine.Widgets.Widget
{
    public PlayerDisplayer(Vec2 position): base(position)
    {
        AddChild(new Frame(Vec2.Zero, new Vec2(600, 500), new Vec2(5), Color.Black));
        AddChild(new Image(new Vec2(0, -150), "player", null, new Rect(16, 0, 16, 28), false, false, new Vec2(4)));
        AddChild(new Label(new Vec2(0, -30), $"Vie : {WS.PlayerData.Stats.Life}", "medium"));
        AddChild(new Label(new Vec2(0, 20), $"Vitesse : {WS.PlayerData.Stats.Speed}", "medium"));
        AddChild(new Label(new Vec2(0, 70), $"Attaque : {WS.PlayerData.Stats.Attack}", "medium"));
        AddChild(new Label(new Vec2(0, 140), $"Arme active : {(WS.PlayerData.ActiveWeapon == null ? "Aucune" : WS.PlayerData.ActiveWeapon?.Name)}", "small"));
        AddChild(new Label(new Vec2(0, 170), GetPassiveWeapons(), "small"));
    }

    private string GetPassiveWeapons()
    {
        var result = "Armes Passives : ";
        var founded = false;
        for (var i = 0; i < 5; i++)
        {
            if (WS.PlayerData.PassiveWeapons[i] == null) continue;
            
            result += WS.PlayerData.PassiveWeapons[i]?.Name + ", ";
            founded = true;
        }

        if (!founded)
            result += "Aucunes";
        else
            result = result.Substring(0, result.Length - 2);

        return result;
    }
}