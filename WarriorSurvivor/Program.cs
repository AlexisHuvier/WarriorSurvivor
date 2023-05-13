using ImGuiNET;
using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
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
                    ImGui.Text($"Player Coords : {(win.IndexCurrentScene == 1 ? win.CurrentScene.GetEntities()[1].GetComponent<TransformComponent>().Position : "Nop")}");
                    ImGui.End();
                }
            }
        };
        
        win.FontManager.AddFont("big", "Resource/Fonts/basic.ttf", 75);
        win.FontManager.AddFont("medium", "Resource/Fonts/basic.ttf", 50);
        win.FontManager.AddFont("small", "Resource/Fonts/basic.ttf", 35);
        
        win.TextureManager.AddTexture("player", "Resource/Sprites/test.png");
        win.TextureManager.AddTexture("bg", "Resource/Sprites/warrior_bg.png");
        
        win.AddScene(new MainMenu());
        win.AddScene(new Game());

        win.IndexCurrentScene = 0;
        win.Run();
    }
}
