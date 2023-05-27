using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace WarriorSurvivor.Scene;

public class Options: SharpEngine.Scene
{
    private readonly Slider _musicSlider;
    private readonly Slider _soundSlider;
    private readonly Button _leftButton;
    private readonly Button _rightButton;
    private readonly Button _upButton;
    private readonly Button _downButton;

    private int _waitForInput = -1;

    public Options()
    {
        AddWidget(new Label(new Vec2(600, 150), "Options", "big"));
        AddWidget(new Label(new Vec2(600, 275), "Volume Musique", "small"));
        _musicSlider = AddWidget(new Slider(new Vec2(600, 300), Color.CornflowerBlue, new Vec2(250, 25), "small"));
        AddWidget(new Label(new Vec2(600, 350), "Volume Son", "small"));
        _soundSlider = AddWidget(new Slider(new Vec2(600, 375), Color.CornflowerBlue, new Vec2(250, 25), "small"));

        AddWidget(new Label(new Vec2(400, 425), "Gauche", "small"));
        _leftButton = AddWidget(new Button(new Vec2(400, 475), "Left", "small", new Vec2(150, 50)));
        _leftButton.Command = WaitForInput;
        AddWidget(new Label(new Vec2(800, 425), "Droite", "small"));
        _rightButton = AddWidget(new Button(new Vec2(800, 475), "Right", "small", new Vec2(150, 50)));
        _rightButton.Command = WaitForInput;
        AddWidget(new Label(new Vec2(400, 525), "Haut", "small"));
        _upButton = AddWidget(new Button(new Vec2(400, 575), "Up", "small", new Vec2(150, 50)));
        _upButton.Command = WaitForInput;
        AddWidget(new Label(new Vec2(800, 525), "Bas", "small"));
        _downButton = AddWidget(new Button(new Vec2(800, 575), "Down", "small", new Vec2(150, 50)));
        _downButton.Command = WaitForInput;

        AddWidget(new Button(new Vec2(600, 700), "Retour", "small", new Vec2(250, 50))).Command = BackCommand;

        OpenScene = InitValues;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_waitForInput == -1)
        {
            var mediaVolume = _musicSlider.Value / 100f;
            var soundVolume = _soundSlider.Value / 100f;

            if (!(Math.Abs(mediaVolume - MediaPlayer.Volume) > 0.0001f) &&
                !(Math.Abs(soundVolume - SoundEffect.MasterVolume) > 0.0001f)) return;
            
            MediaPlayer.Volume = _musicSlider.Value / 100f;
            SoundEffect.MasterVolume = _soundSlider.Value / 100f;
            WS.OptionsManager.WriteSave(GetWindow());
        }
        else
        {

            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Length == 0) return;

            var controlComponent = GetWindow().GetScene<Game>(1).Player.GetComponent<ControlComponent>();
            switch (_waitForInput)
            {
                case 0:
                    if (keys[0] != Keys.Escape)
                        controlComponent.SetKey(ControlKey.Left, (Key)keys[0]);
                    _leftButton.Text = controlComponent.GetKey(ControlKey.Left).ToString();
                    break;
                case 1:
                    if (keys[0] != Keys.Escape)
                        controlComponent.SetKey(ControlKey.Right, (Key)keys[0]);
                    _rightButton.Text = controlComponent.GetKey(ControlKey.Right).ToString();
                    break;
                case 2:
                    if (keys[0] != Keys.Escape)
                        controlComponent.SetKey(ControlKey.Up, (Key)keys[0]);
                    _upButton.Text = controlComponent.GetKey(ControlKey.Up).ToString();
                    break;
                case 3:
                    if (keys[0] != Keys.Escape)
                        controlComponent.SetKey(ControlKey.Down, (Key)keys[0]);
                    _downButton.Text = controlComponent.GetKey(ControlKey.Down).ToString();
                    break;
            }
            
            WS.OptionsManager.WriteSave(GetWindow());
            _waitForInput = -1;
        }
    }

    private void WaitForInput(Button button)
    {
        if (button == _leftButton)
            _waitForInput = 0;
        else if (button == _rightButton)
            _waitForInput = 1;
        else if (button == _upButton)
            _waitForInput = 2;
        else if (button == _downButton)
            _waitForInput = 3;
        button.Text = "...";
    }

    private void InitValues(SharpEngine.Scene _)
    {
        _musicSlider.Value = (int)(MediaPlayer.Volume * 100);
        _soundSlider.Value = (int)(SoundEffect.MasterVolume * 100);
        
        var controlComponent = GetWindow().GetScene<Game>(1).Player.GetComponent<ControlComponent>();
        _leftButton.Text = controlComponent.GetKey(ControlKey.Left).ToString();
        _rightButton.Text = controlComponent.GetKey(ControlKey.Right).ToString();
        _upButton.Text = controlComponent.GetKey(ControlKey.Up).ToString();
        _downButton.Text = controlComponent.GetKey(ControlKey.Down).ToString();
    }

    private void BackCommand(Button _)
    {
        if (_waitForInput != -1) return;
        GetWindow().IndexCurrentScene = 0;
    }
}