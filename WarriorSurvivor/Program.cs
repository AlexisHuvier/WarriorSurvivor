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
        var win = new Window(new Vec2(1200, 900), Color.CornflowerBlue, debug: true)
        {
            RenderImGui = win =>
            {
                DebugManager.CreateSharpEngineImGuiWindow();
                {
                    ImGui.Begin("Warrior Survivor Information");
                    ImGui.Text($"Current Scene : {win.IndexCurrentScene}");
                    ImGui.Separator();
                    if (win.IndexCurrentScene == 1 && ImGui.TreeNode("Game Information"))
                    {
                        ImGui.Text($"Active Weapon Speed : {win.GetCurrentScene<Game>().ActiveWeapon.MovingValue}");
                        ImGui.Separator();
                        var chests = win.GetCurrentScene<Game>().Chests;
                        ImGui.Text($"Chest Pos : {(chests.Count > 0 ? chests[0].GetComponent<TransformComponent>().Position : "Nop")}");
                        ImGui.Text($"Enemy Timer : {((Map)win.CurrentScene.GetEntities()[0]).GetComponent<SpawnerComponent>().GetEnemyTimer()}");
                        ImGui.Text($"Enemy Count : {win.GetCurrentScene<Game>().Enemies.Count}");
                        ImGui.TreePop();
                    }

                    ImGui.End();
                }
            }
        };
        
        // === RESOURCES ===
        
        // POLICES
        win.FontManager.AddFont("big", "Resource/Fonts/basic.ttf", 75);
        win.FontManager.AddFont("medium", "Resource/Fonts/basic.ttf", 50);
        win.FontManager.AddFont("small", "Resource/Fonts/basic.ttf", 35);
        
        
        // SOUND EFFECTS
        SoundManager.AddSound("exp_point-pop", "Resource/Sounds/exp_point-pop.wav");
        SoundManager.AddSound("equip", "Resource/Sounds/equip.wav");
        SoundManager.AddSound("chest", "Resource/Sounds/chest.wav");
        
        
        // MUSICS
        MusicManager.AddSong("game-music", new Uri("Resource/Musics/game-music.ogg", UriKind.Relative));
        
        
        // TEXTURES
        // General
        win.TextureManager.AddTexture("player", "Resource/Sprites/KnightM.png");
        win.TextureManager.AddTexture("bg", "Resource/Sprites/BG.png");
        win.TextureManager.AddTexture("enemy", "Resource/Sprites/Orc.png");
        win.TextureManager.AddTexture("chest", "Resource/Sprites/Chest.png");
        
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

        win.IndexCurrentScene = 0;
        win.Run();
    }
}
