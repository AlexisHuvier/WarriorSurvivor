using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Widget;

namespace WarriorSurvivor.Scene;

public class GameMenu: SharpEngine.Scene
{
    private readonly PlayerDisplayer _playerDisplayer;
    
    public GameMenu()
    {
        _playerDisplayer = AddWidget(new PlayerDisplayer(new Vec2(600, 400)));
        AddWidget(new Button(new Vec2(400, 800), "Lancer une run", "small", new Vec2(200, 50))).Command = LaunchCommand;
        AddWidget(new Button(new Vec2(800, 800), "Menu", "small", new Vec2(200, 50))).Command = MenuCommand;
        OpenScene = OpenSceneCallback;
    }
    
    private void OpenSceneCallback(SharpEngine.Scene _) => _playerDisplayer.UpdateInformation();

    private void LaunchCommand(Button _)
    {
        ((Game)GetWindow().GetScene(1)).Init();
        GetWindow().IndexCurrentScene = 1;
    }

    private void MenuCommand(Button _)
    {
        WS.SaveManager.WriteSave();
        GetWindow().IndexCurrentScene = 0;
    }
}