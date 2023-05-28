using ImGuiNET;
using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor;

internal static class Program
{
    private static void Main()
    {
        var win = new Window(new Vec2(1200, 900), Color.CornflowerBlue, exitWithEscape: false);
        
        // === RESOURCES ===
        
        // POLICES
        win.FontManager.AddFont("big", "Resource/Fonts/basic.ttf", 75);
        win.FontManager.AddFont("medium", "Resource/Fonts/basic.ttf", 50);
        win.FontManager.AddFont("small", "Resource/Fonts/basic.ttf", 35);
        
        
        // SOUND EFFECTS
        SoundManager.AddSound("exp_point-pop", "Resource/Sounds/exp_point-pop.wav");
        SoundManager.AddSound("equip", "Resource/Sounds/equip.wav");
        SoundManager.AddSound("chest", "Resource/Sounds/chest.wav");
        SoundManager.AddSound("enemy-hit", "Resource/Sounds/enemy-hit.wav");
        SoundManager.AddSound("player-hit", "Resource/Sounds/player-hit.wav");
        SoundManager.AddSound("beep", "Resource/Sounds/beep.wav");
        SoundManager.AddSound("gold", "Resource/Sounds/gold.wav");
        SoundManager.AddSound("boss", "Resource/Sounds/boss.wav");
        
        
        // MUSICS
        MusicManager.AddSong("game-music", new Uri("Resource/Musics/game-music.ogg", UriKind.Relative));
        
        
        // TEXTURES
        // General
        win.TextureManager.AddTexture("player", "Resource/Sprites/KnightM.png");
        win.TextureManager.AddTexture("bg", "Resource/Sprites/BG.png");
        win.TextureManager.AddTexture("enemy", "Resource/Sprites/Orc.png");
        win.TextureManager.AddTexture("chest", "Resource/Sprites/Chest.png");
        win.TextureManager.AddTexture("boss", "Resource/Sprites/Boss.png");
        win.TextureManager.AddTexture("rock", "Resource/Sprites/Rock.png");
        
        // Weapons
        win.TextureManager.AddTexture("weapon-null", "Resource/Sprites/Weapons/icon-null.png");
        win.TextureManager.AddTexture("weapon-bottes_ailees", "Resource/Sprites/Weapons/icon-bottes_ailees.png");
        win.TextureManager.AddTexture("weapon-cristal_vie", "Resource/Sprites/Weapons/icon-cristal_vie.png");
        win.TextureManager.AddTexture("weapon-haltere", "Resource/Sprites/Weapons/icon-haltere.png");
        win.TextureManager.AddTexture("weapon-cercle_feu", "Resource/Sprites/Weapons/icon-cercle_feu.png");
        win.TextureManager.AddTexture("weapon-couteau", "Resource/Sprites/Weapons/icon-couteau.png");
        
        // === RESOURCES ===
        
        win.AddScene(new MainMenu());
        win.AddScene(new Game());
        win.AddScene(new GameMenu());
        win.AddScene(new Options());

        win.IndexCurrentScene = 0;
        win.Run();
    }
}
