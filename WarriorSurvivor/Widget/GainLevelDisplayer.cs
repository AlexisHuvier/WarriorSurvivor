using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Data;
using WarriorSurvivor.Data.DB;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Widget;

public class GainLevelDisplayer: SharpEngine.Widgets.Widget
{
    private readonly Label _leftTitle;
    private readonly Label _rightTitle;
    private readonly Label _leftDescription;
    private readonly Label _rightDescription;
    private readonly Button _leftButton;
    private readonly Button _rightButton;
    
    public GainLevelDisplayer(): base(new Vec2(600, 450))
    {
        Displayed = false;
        PauseState = PauseState.Enabled;
        ZLayer = 4000;
        
        AddChild(new Frame(Vec2.Zero, new Vec2(800, 600), new Vec2(5), Color.Black, Color.CornflowerBlue)).ZLayer = 4000;
        AddChild(new Label(new Vec2(0, -240), "Passage de Niveau !", "medium"));
        AddChild(new Frame(new Vec2(-200, 35), new Vec2(300, 400), new Vec2(5), Color.Black, Color.CornflowerBlue));
        AddChild(new Frame(new Vec2(200, 35), new Vec2(300, 400), new Vec2(5), Color.Black, Color.CornflowerBlue));

        _leftTitle = AddChild(new Label(new Vec2(-200, -120), "Arme 1", "medium"));
        _leftTitle.ZLayer = 4002;
        _rightTitle = AddChild(new Label(new Vec2(200, -120), "Arme 2", "medium"));
        _rightTitle.ZLayer = 4002;

        _leftDescription = AddChild(new Label(new Vec2(-200, 35), "Description 1", "small"));
        _leftDescription.ZLayer = 4002;
        _rightDescription = AddChild(new Label(new Vec2(200, 35), "Description 2", "small"));
        _rightDescription.ZLayer = 4002;
        
        _leftButton = AddChild(new Button(new Vec2(-200, 190), "Choisir", "small"));
        _leftButton.ZLayer = 4002;
        _rightButton = AddChild(new Button(new Vec2(200, 190), "Choisir", "small"));
        _rightButton.ZLayer = 4002;
    }

    public void Reset()
    {
        var nullPassiveWeapon = WS.PlayerData.GetNumberNullPassiveWeapon();
        switch (nullPassiveWeapon)
        {
            case 0:
            {
                var leftData = WS.PlayerData.GetRandomNotNullPassiveWeapon();
                var leftWeapon = Weapon.PassiveWeapons[leftData.Value.Name];
                _leftTitle.Text = leftWeapon.Name;
                _leftDescription.Text = leftWeapon.Description;
                _leftButton.Command = _ =>
                    SetPassiveWeapon(
                        new WeaponData(leftWeapon.Name, GetNextLevelStats(leftWeapon.BaseStats, leftData.Value.Stats)),
                        leftData.Key);

                var rightData = WS.PlayerData.GetRandomNotNullPassiveWeapon();
                var rightWeapon = Weapon.PassiveWeapons[leftData.Value.Name];
                _rightTitle.Text = rightWeapon.Name;
                _rightDescription.Text = rightWeapon.Description;
                _rightButton.Command = _ => 
                    SetPassiveWeapon(
                        new WeaponData(rightWeapon.Name, GetNextLevelStats(rightWeapon.BaseStats, rightData.Value.Stats)),
                        rightData.Key);
                break;
            }
            case 5:
            {
                var leftWeapon = Weapon.GetPassiveWeaponWhichPlayerNotHave();
                _leftTitle.Text = leftWeapon.Name;
                _leftDescription.Text = leftWeapon.Description;
                _leftButton.Command = _ => SetPassiveWeapon(new WeaponData(leftWeapon.Name, leftWeapon.BaseStats));
            
                var rightWeapon = Weapon.GetPassiveWeaponWhichPlayerNotHave();
                _rightTitle.Text = rightWeapon.Name;
                _rightDescription.Text = rightWeapon.Description;
                _rightButton.Command = _ => SetPassiveWeapon(new WeaponData(rightWeapon.Name, rightWeapon.BaseStats));
                break;
            }
            default:
            {
                var leftData = WS.PlayerData.GetRandomNotNullPassiveWeapon();
                var leftWeapon = Weapon.PassiveWeapons[leftData.Value.Name];
                _leftTitle.Text = leftWeapon.Name;
                _leftDescription.Text = leftWeapon.Description;
                _leftButton.Command = _ =>
                    SetPassiveWeapon(
                        new WeaponData(leftWeapon.Name, GetNextLevelStats(leftWeapon.BaseStats, leftData.Value.Stats)),
                        leftData.Key);
            
                var rightWeapon = Weapon.GetPassiveWeaponWhichPlayerNotHave();
                _rightTitle.Text = rightWeapon.Name;
                _rightDescription.Text = rightWeapon.Description;
                _rightButton.Command = _ => SetPassiveWeapon(new WeaponData(rightWeapon.Name, rightWeapon.BaseStats));
                break;
            }
        }
    }

    private Stats GetNextLevelStats(Stats baseStats, Stats currentStats)
    {
        var currentLevel = currentStats.Level;
        currentLevel++;
        return baseStats.Multiply(currentLevel);
    }

    private void SetPassiveWeapon(WeaponData data, int nb = -1)
    {
        if (nb == -1)
        {
            for (var i = 0; i < 5; i++)
            {
                if (WS.PlayerData.PassiveWeapons[i] != null) continue;
                
                nb = i;
                break;
            }
        }
        
        WS.PlayerData.PassiveWeapons[nb] = data;

        var entity = Weapon.PassiveWeapons[data.Name].GetEntity();
        if(entity is not null)
            GetScene<Game>().SetPassiveWeapon((SharpEngine.Entities.Entity)Activator.CreateInstance(entity, data)!, nb);

        GetScene<Game>().Player.TakeDamage(0);
        Displayed = false;
        GetScene<Game>().Paused = false;
        SoundManager.Play("equip");
    }
}