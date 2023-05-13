using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Scene;

public class MainMenu: SharpEngine.Scene
{
    public MainMenu()
    {
        AddWidget(new Label(new Vec2(600, 200), "Warrior Survivor", "big"));
        AddWidget(new Button(new Vec2(600, 400), "Jouer", "small", new Vec2(200, 50))).Command =
            button => button.GetWindow().IndexCurrentScene = 1;
        AddWidget(new Button(new Vec2(600, 500), "Options", "small", new Vec2(200, 50)));
        AddWidget(new Button(new Vec2(600, 600), "Quitter", "small", new Vec2(200, 50))).Command =
            button => button.GetWindow().Stop();
    }
}