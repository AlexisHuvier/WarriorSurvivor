using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Scene;

public class MainMenu: SharpEngine.Scene
{
    public MainMenu()
    {
        AddWidget(new Label(new Vec2(600, 200), "Warrior Survivor", "big"));
        AddWidget(new Button(new Vec2(600, 350), "Jouer", "small", new Vec2(250, 50))).Command = PlayCommand;
        AddWidget(new Button(new Vec2(600, 450), "Supprimer Données", "small", new Vec2(250, 50))).Command = RemoveCommand;
        AddWidget(new Button(new Vec2(600, 550), "Options", "small", new Vec2(250, 50)));
        AddWidget(new Button(new Vec2(600, 650), "Quitter", "small", new Vec2(250, 50))).Command = QuitCommand;
        
        WS.SaveManager.Init();
    }

    private void QuitCommand(Button _) => GetWindow().Stop();
    private static void RemoveCommand(Button _) => WS.SaveManager.Reset();
    private void PlayCommand(Button _) => GetWindow().IndexCurrentScene = 2;

    public override void Initialize()
    {
        base.Initialize();
        MusicManager.SetRepeating(true);
        MusicManager.Play("game-music");
    }

    public override void UnloadContent()
    {
        WS.SaveManager.WriteSave();
        base.UnloadContent();
    }
}