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
    private int _rotation;
    
    public ExpDisplayComponent()
    { }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _rotation++;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        var zLayer = GetEntity().GetComponent<TransformComponent>().ZLayer / 4096f;
        var position = GetEntity().GetComponent<TransformComponent>().Position - CameraManager.Position;
        var texture = GetWindow().TextureManager.GetTexture("blank");
        
        Renderer.RenderTexture(GetWindow(), texture, position, null, Color.MediumAquamarine, MathHelper.ToRadians(_rotation), new Vec2(0.5f), Size, SpriteEffects.None, zLayer);
        Renderer.RenderTexture(GetWindow(), texture, position, null, Color.MediumAquamarine, MathHelper.ToRadians(_rotation + 45), new Vec2(0.5f), Size, SpriteEffects.None, zLayer);
    }
}