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
    private TransformComponent _transformComponent = null!;

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = GetEntity().GetComponent<TransformComponent>();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        var zLayer = GetEntity().GetComponent<TransformComponent>().ZLayer / 4096f;

        var blankTexture = GetWindow().TextureManager.GetTexture("blank");
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(
                _transformComponent.Position.X + _offset.X - CameraManager.Position.X - _size.X / 2,
                _transformComponent.Position.Y + _offset.Y - CameraManager.Position.Y - _size.Y / 2,
                _size.X, _size.Y), Color.Black, zLayer - 0.00003f);
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(
                _transformComponent.Position.X + _offset.X - CameraManager.Position.X - (_size.X - 4) / 2,
                _transformComponent.Position.Y + _offset.Y - CameraManager.Position.Y - (_size.Y - 4) / 2,
                _size.X - 4, _size.Y - 4), Color.White, zLayer - 0.00002f);
        Renderer.RenderTexture(GetWindow(), blankTexture, 
            new Rect(
                _transformComponent.Position.X + _offset.X - CameraManager.Position.X - (_size.X - 8) / 2,
                _transformComponent.Position.Y + _offset.Y - CameraManager.Position.Y - (_size.Y - 8) / 2,
                (_size.X - 8) * Value / 100, _size.Y - 8),
            Color.Green, zLayer - 0.00001f);
    }
}