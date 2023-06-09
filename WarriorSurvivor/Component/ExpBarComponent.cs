using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Component;

public class ExpBarComponent: SharpEngine.Components.Component
{
    private readonly Vec2 _size = new(400, 30);

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        var value = (float)WS.PlayerData.Exp / WS.PlayerData.GetExpToNextLevel();

        var blankTexture = GetWindow().TextureManager.GetTexture("blank");
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(250 - _size.X / 2, 40 - _size.Y / 2, _size), 
            Color.Black, 1 - 0.00003f);
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(250 - (_size.X - 4) / 2, 40 - (_size.Y - 4) / 2,_size.X - 4, _size.Y - 4), 
            Color.White, 1 - 0.00002f);
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(250 - (_size.X - 8) / 2, 40 - (_size.Y - 8) / 2, (_size.X - 8) * value, _size.Y - 8), 
            Color.MediumAquamarine, 1 - 0.00001f);
    }
}