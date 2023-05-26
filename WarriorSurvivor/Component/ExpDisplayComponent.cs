using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Components;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace WarriorSurvivor.Component;

public class ExpDisplayComponent: SharpEngine.Components.Component
{
    private static readonly Vec2 Size = new(25);
    private float _rotation;
    private TransformComponent _transformComponent = null!;

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = GetEntity().GetComponent<TransformComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 300;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        var zLayer = _transformComponent.ZLayer / 4096f;
        var position = _transformComponent.Position - CameraManager.Position;
        var texture = GetWindow().TextureManager.GetTexture("blank");
        
        Renderer.RenderTexture(GetWindow(), texture, position, null, Color.MediumAquamarine, MathHelper.ToRadians(_rotation), new Vec2(0.5f), Size, SpriteEffects.None, zLayer);
        Renderer.RenderTexture(GetWindow(), texture, position, null, Color.MediumAquamarine, MathHelper.ToRadians(_rotation + 45), new Vec2(0.5f), Size, SpriteEffects.None, zLayer);
    }
}