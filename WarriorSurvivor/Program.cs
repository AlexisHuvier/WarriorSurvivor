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
                    if (WS.SaveManager.Save != null && ImGui.TreeNode("Save Information"))
                    {
                        ImGui.Text($"PLayer Life :  {WS.PlayerData.Life}");
                        ImGui.Text($"Player Stats : {WS.PlayerData.Stats}");
                        ImGui.Text($"Player Active Weapon : {WS.PlayerData.ActiveWeapon}");
                        for(var i = 0; i < 5; i++)
                            ImGui.Text($"Player Passive Weapon {i} : {WS.PlayerData.PassiveWeapons[i]}");
                        ImGui.TreePop();
                    }

                    if (win.IndexCurrentScene == 2 && ImGui.TreeNode("GameMenu Information"))
                    {
                        ImGui.Text($"Frame Position : {win.CurrentScene.GetWidgets()[0].GetChildren()[0].GetRealPosition()}");
                        ImGui.TreePop();
                    }
                    if (win.IndexCurrentScene == 1 && ImGui.TreeNode("Game Information"))
                    {
                        var chests = win.GetCurrentScene<Game>().Chests;
                        ImGui.Text($"Chest Position : {(chests.Count > 0 ? chests[0].GetComponent<TransformComponent>().Position : "Nop")}");
                        ImGui.Text($"Enemy Timer : {((Map)win.CurrentScene.GetEntities()[0]).GetComponent<SpawnerComponent>().GetEnemyTimer()}");
                        ImGui.Text($"Enemy Count : {win.GetCurrentScene<Game>().Enemies.Count}");
                        ImGui.Text(
                            $"Player Coords : {win.CurrentScene.GetEntities()[1].GetComponent<TransformComponent>().Position}");
                        ImGui.TreePop();
                    }

                    ImGui.End();
                }
            }
        };
        
        win.FontManager.AddFont("big", "Resource/Fonts/basic.ttf", 75);
        win.FontManager.AddFont("medium", "Resource/Fonts/basic.ttf", 50);
        win.FontManager.AddFont("small", "Resource/Fonts/basic.ttf", 35);
        
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
        
        win.AddScene(new MainMenu());
        win.AddScene(new Game());
        win.AddScene(new GameMenu());

        win.IndexCurrentScene = 0;
        win.Run();
    }
}
