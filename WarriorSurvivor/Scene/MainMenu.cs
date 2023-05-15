using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Scene;

public class MainMenu: SharpEngine.Scene
{
    public MainMenu()
    {
        AddWidget(new Label(new Vec2(600, 200), "Warrior Survivor", "big"));
        AddWidget(new Button(new Vec2(600, 350), "Jouer", "small", new Vec2(200, 50))).Command =
            _ => GetWindow().IndexCurrentScene = 2;
        AddWidget(new Button(new Vec2(600, 450), "Supprimer Data", "small", new Vec2(200, 50))).Command =
            _ => WS.SaveManager.Reset();
        AddWidget(new Button(new Vec2(600, 550), "Options", "small", new Vec2(200, 50)));
        AddWidget(new Button(new Vec2(600, 650), "Quitter", "small", new Vec2(200, 50))).Command =
            _ => GetWindow().Stop();
        
        WS.SaveManager.Init();
    }

    public override void UnloadContent()
    {
        WS.SaveManager.WriteSave();
        base.UnloadContent();
    }
}