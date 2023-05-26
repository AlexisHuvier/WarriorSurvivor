using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Data.DB;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Widget;

public class ChestDisplayer: SharpEngine.Widgets.Widget
{
    private readonly Label[] _labels = new Label[5];
    private readonly string[] _texts = new string[5];
    private readonly Button _validButton;
    private int _nbLancer;
    private double _timer;
    
    public ChestDisplayer(): base(new Vec2(600, 450))
    {
        Displayed = false;
        PauseState = PauseState.Enabled;
        ZLayer = 4000;

        AddChild(new Frame(Vec2.Zero, new Vec2(970, 400), new Vec2(20, 20), Color.Black, Color.CornflowerBlue))
                .ZLayer = 4000;

        AddChild(new Frame(new Vec2(-340, -50), new Vec2(150, 200), new Vec2(10), Color.Black, Color.CornflowerBlue))
            .ZLayer = 4001;
        AddChild(new Frame(new Vec2(-170, -50), new Vec2(150, 200), new Vec2(10), Color.Black, Color.CornflowerBlue))
            .ZLayer = 4001;
        AddChild(new Frame(new Vec2(0, -50), new Vec2(150, 200), new Vec2(10), Color.Red, Color.CornflowerBlue))
            .ZLayer = 4001;
        AddChild(new Frame(new Vec2(170, -50), new Vec2(150, 200), new Vec2(10), Color.Black, Color.CornflowerBlue))
            .ZLayer = 4001;
        AddChild(new Frame(new Vec2(340, -50), new Vec2(150, 200), new Vec2(10), Color.Black, Color.CornflowerBlue))
            .ZLayer = 4001;

        _labels[0] = AddChild(new Label(new Vec2(-340, -50), "", "small"));
        _labels[1] = AddChild(new Label(new Vec2(-170, -50), "", "small"));
        _labels[2] = AddChild(new Label(new Vec2(0, -50), "", "small"));
        _labels[3] = AddChild(new Label(new Vec2(170, -50), "", "small"));
        _labels[4] = AddChild(new Label(new Vec2(340, -50), "", "small"));

        for (var i = 0; i < 5; i++)
        {
            _labels[i].ZLayer = 4002;
            _texts[i] = GetRandomPrize();
            _labels[i].Text = _texts[i];
        }

        _validButton = AddChild(new Button(new Vec2(0, 125), "Lancer", "small", new Vec2(300, 50)));
        _validButton.ZLayer = 4001;
        _validButton.Command = ValidButtonCommand;
    }

    private void ValidButtonCommand(Button button)
    {
        if (button.Text == "Lancer")
        {
            _nbLancer = Rand.GetRand(15, 20);
            _timer = 0;
            button.Active = false;
        }
        else
        {
            switch (_labels[2].Text)
            {
                case "Potion Vie":
                    GetScene<Game>().Player.TakeDamage(-10);
                    break;
                case "Potion Mort":
                    foreach (var enemy in GetScene<Game>().Enemies) GetScene<Game>().RemoveEntity(enemy, true);
                    GetScene<Game>().Enemies.Clear();
                    break;
                case "Arme":
                    GetScene<Game>().SetActiveWeapon(Weapon.ActiveWeapons[GetScene<Game>().ActiveWeapon.Data.Name]);
                    break;
                case "Or":
                    WS.PlayerData.ModifyGold(-10);
                    break;
                case "XP":
                    if (WS.PlayerData.AddExp(10)) GetScene<Game>().GainLevel();
                    break;
            }

            Displayed = false;
            GetScene<Game>().Paused = false;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if(_nbLancer == 0) return;

        if (_timer >= 0.1 + 0.5 / _nbLancer)
        {
            _timer = 0;
            _nbLancer -= 1;
            UpdateTexts();
            if (_nbLancer == 0)
            {
                _validButton.Text = "Valider";
                _validButton.Active = true;
            }
        }

        _timer += gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void UpdateTexts()
    {
        for (var i = 0; i < 4; i++)
        {
            _texts[i] = _texts[i + 1];
            _labels[i].Text = _texts[i];
        }

        _texts[4] = GetRandomPrize();
        _labels[4].Text = _texts[4];
    }

    public void Reset()
    {
        for (var i = 0; i < 5; i++)
        {
            _texts[i] = GetRandomPrize();
            _labels[i].Text = _texts[i];
        }

        _validButton.Text = "Lancer";
    }

    private static string GetRandomPrize()
    {
        return Rand.GetRand(5) switch
        {
            0 => "Or",
            1 => "Arme",
            2 => "XP",
            3 => "Potion Vie",
            _ => "Potion Mort"
        };
    }
}