using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Scene;

public class GameMenu: SharpEngine.Scene
{
    public GameMenu()
    {
        AddWidget(new Button(new Vec2(600, 450), "Lancer une run", "small", new Vec2(200, 50))).Command =
            _ =>
            {
                ((Game)GetWindow().GetScene(1)).Init();
                GetWindow().IndexCurrentScene = 1;
            };
        AddWidget(new Button(new Vec2(600, 550), "Menu", "small", new Vec2(200, 50))).Command =
            _ =>
            {
                WS.SaveManager.WriteSave();
                GetWindow().IndexCurrentScene = 0;
            };
    }
}