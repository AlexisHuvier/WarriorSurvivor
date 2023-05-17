using SharpEngine.Components;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;

namespace WarriorSurvivor.Component;

public class LifeBarComponent: SharpEngine.Components.Component
{
    public float Value = 100;
    
    private readonly Vec2 _size = new(150, 20);
    private readonly Vec2 _offset = new(0, 50);

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        var zLayer = GetEntity().GetComponent<TransformComponent>().ZLayer / 4096f;
        var position = GetEntity().GetComponent<TransformComponent>().Position + _offset - CameraManager.Position;

        var blankTexture = GetWindow().TextureManager.GetTexture("blank");
        Renderer.RenderTexture(GetWindow(), blankTexture, new Rect(position - _size / 2, _size), Color.Black, zLayer - 0.00003f);
        Renderer.RenderTexture(GetWindow(), blankTexture, new Rect(position - new Vec2((_size.X - 4) / 2, (_size.Y - 4) / 2), new Vec2(_size.X - 4, _size.Y - 4)), Color.White, zLayer - 0.00002f);
        var barSize = new Vec2((_size.X - 8) * Value / 100, _size.Y - 8);
        Renderer.RenderTexture(GetWindow(), blankTexture, new Rect(position - new Vec2(_size.X - 8, _size.Y - 8) / 2, barSize), Color.Green, zLayer - 0.00001f);
    }
}