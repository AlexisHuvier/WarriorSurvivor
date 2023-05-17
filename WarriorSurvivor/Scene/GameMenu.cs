using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Widget;

namespace WarriorSurvivor.Scene;

public class GameMenu: SharpEngine.Scene
{
    public GameMenu()
    {
        AddWidget(new PlayerDisplayer(new Vec2(600, 350)));
        AddWidget(new Button(new Vec2(600, 700), "Lancer une run", "small", new Vec2(200, 50))).Command =
            _ =>
            {
                ((Game)GetWindow().GetScene(1)).Init();
                GetWindow().IndexCurrentScene = 1;
            };
        AddWidget(new Button(new Vec2(600, 800), "Menu", "small", new Vec2(200, 50))).Command =
            _ =>
            {
                WS.SaveManager.WriteSave();
                GetWindow().IndexCurrentScene = 0;
            };
    }
}