using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Widget;

namespace WarriorSurvivor.Scene;

public class GameMenu: SharpEngine.Scene
{
    private readonly PlayerDisplayer _playerDisplayer;
    private readonly Button _lifeButton;
    private readonly Button _speedButton;
    private readonly Button _attackButton;
    
    public GameMenu()
    {
        _playerDisplayer = AddWidget(new PlayerDisplayer(new Vec2(600, 400)));
        AddWidget(new Button(new Vec2(400, 800), "Lancer une run", "small", new Vec2(200, 50))).Command = LaunchCommand;
        AddWidget(new Button(new Vec2(800, 800), "Menu", "small", new Vec2(200, 50))).Command = MenuCommand;

        _lifeButton = AddWidget(new Button(new Vec2(600, 475), "Améliorer Vie (100 or)", "small", new Vec2(400, 50)));
        _speedButton = AddWidget(new Button(new Vec2(600, 550), "Améliorer Vitesse (100 or)", "small", new Vec2(400, 50)));
        _attackButton = AddWidget(new Button(new Vec2(600, 625), "Améliorer Attaque (100 or)", "small", new Vec2(400, 50)));

        _lifeButton.Command = UpgradeButtonCommand;
        _speedButton.Command = UpgradeButtonCommand;
        _attackButton.Command = UpgradeButtonCommand;
        
        OpenScene = OpenSceneCallback;
    }

    private void OpenSceneCallback(SharpEngine.Scene _)
    {
        _playerDisplayer.UpdateInformation();
        _lifeButton.Text = $"Améliorer Vie ({100 * (WS.PlayerData.UpgradeBuy[0] + 1)} or)";
        _speedButton.Text = $"Améliorer Vitesse ({100 * (WS.PlayerData.UpgradeBuy[1] + 1)} or)";
        _attackButton.Text = $"Améliorer Attaque ({100 * (WS.PlayerData.UpgradeBuy[2] + 1)} or)";

        _lifeButton.Active = WS.PlayerData.Gold >= 100 * (WS.PlayerData.UpgradeBuy[0] + 1);
        _speedButton.Active = WS.PlayerData.Gold >= 100 * (WS.PlayerData.UpgradeBuy[1] + 1);
        _attackButton.Active = WS.PlayerData.Gold >= 100 * (WS.PlayerData.UpgradeBuy[2] + 1);
        
        WS.SaveManager.WriteSave();
    }

    private void UpgradeButtonCommand(Button button)
    {
        if (button == _lifeButton)
        {
            var cost = 100 * (WS.PlayerData.UpgradeBuy[0] + 1);
            if (!WS.PlayerData.ModifyGold(cost)) return;
            
            WS.PlayerData.Stats.Life += 10;
            WS.PlayerData.UpgradeBuy[0]++;
        }
        else if (button == _speedButton)
        {
            var cost = 100 * (WS.PlayerData.UpgradeBuy[1] + 1);
            if (!WS.PlayerData.ModifyGold(cost)) return;
            
            WS.PlayerData.Stats.Speed += 50;
            WS.PlayerData.UpgradeBuy[1]++;
        }
        else if (button == _attackButton)
        {
            var cost = 100 * (WS.PlayerData.UpgradeBuy[2] + 1);
            if (!WS.PlayerData.ModifyGold(cost)) return;
            
            WS.PlayerData.Stats.Attack += 2;
            WS.PlayerData.UpgradeBuy[2]++;
        }
        
        OpenSceneCallback(null!);
    }

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