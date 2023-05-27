using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Manager;

public class OptionsManager
{
    private Save? _options;
    
    public void Init(Window window)
    {
        _options = Save.Load("Resource/options.wssave", new Dictionary<string, object>
        {
            { "music_volume", 100 },
            { "sound_volume", 100 },
            { "left", (int)Key.Left },
            { "right", (int)Key.Right },
            { "up", (int)Key.Up },
            { "down", (int)Key.Down }
        });
        
        MusicManager.SetVolume(_options.GetObjectAs("music_volume", 100));
        SoundEffect.MasterVolume = _options.GetObjectAs("sound_volume", 100) / 100f;
        var control = window.GetScene<Game>(1).Player.GetComponent<ControlComponent>();
        control.SetKey(ControlKey.Left, (Key)_options.GetObjectAs("left", (int)Key.Left));
        control.SetKey(ControlKey.Right, (Key)_options.GetObjectAs("right", (int)Key.Right));
        control.SetKey(ControlKey.Up, (Key)_options.GetObjectAs("up", (int)Key.Up));
        control.SetKey(ControlKey.Down, (Key)_options.GetObjectAs("down", (int)Key.Down));
    }

    public void ApplyControls(Player player)
    {
        if(_options == null) return;
        
        var control = player.GetComponent<ControlComponent>();
        control.SetKey(ControlKey.Left, (Key)_options.GetObjectAs("left", (int)Key.Left));
        control.SetKey(ControlKey.Right, (Key)_options.GetObjectAs("right", (int)Key.Right));
        control.SetKey(ControlKey.Up, (Key)_options.GetObjectAs("up", (int)Key.Up));
        control.SetKey(ControlKey.Down, (Key)_options.GetObjectAs("down", (int)Key.Down));
    }

    public void WriteSave(Window window)
    {
        _options?.SetObject("music_volume", (int)(MediaPlayer.Volume * 100));
        _options?.SetObject("sound_volume", (int)(SoundEffect.MasterVolume * 100));
        
        var control = window.GetScene<Game>(1).Player.GetComponent<ControlComponent>();
        _options?.SetObject("left", (int)control.GetKey(ControlKey.Left));
        _options?.SetObject("right", (int)control.GetKey(ControlKey.Right));
        _options?.SetObject("up", (int)control.GetKey(ControlKey.Up));
        _options?.SetObject("down", (int)control.GetKey(ControlKey.Down));

        _options?.Write("Resource/options.wssave");
    }
}