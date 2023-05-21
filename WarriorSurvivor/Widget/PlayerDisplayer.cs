using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Widget;

public class PlayerDisplayer: SharpEngine.Widgets.Widget
{
    public PlayerDisplayer(Vec2 position): base(position)
    {
        UpdateInformation();
    }

    public void UpdateInformation()
    {
        RemoveAllChildren();
        var passiveStats = WS.PlayerData.GetPassiveStats();
        AddChild(new Frame(Vec2.Zero, new Vec2(800, 650), new Vec2(5), Color.Black));
        AddChild(new Image(new Vec2(0, -275), "player", null, new Rect(16, 0, 16, 28), false, false, new Vec2(4)));
        AddChild(new Label(new Vec2(0, -175), $"Vie : {WS.PlayerData.Stats.Life} (+{passiveStats.Life})", "medium"));
        AddChild(new Label(new Vec2(0, -125), $"Vitesse : {WS.PlayerData.Stats.Speed} (+{passiveStats.Speed})", "medium"));
        AddChild(new Label(new Vec2(0, -75), $"Attaque : {WS.PlayerData.Stats.Attack} (+{passiveStats.Attack})", "medium"));
        AddChild(new Label(new Vec2(0, -25), $"Or : {WS.PlayerData.Gold}", "medium"));
    }
}