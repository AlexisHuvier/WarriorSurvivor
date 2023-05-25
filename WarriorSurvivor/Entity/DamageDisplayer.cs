using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Entity;

public class DamageDisplayer: SharpEngine.Entities.Entity
{
    private double _timer = 1;
    
    public DamageDisplayer(Vec2 position, Color color, string text)
    {
        AddComponent(new TransformComponent(position, zLayer: 3500));
        AddComponent(new TextComponent(text, "medium", color));
        AddComponent(new AutoMovementComponent(new Vec2(0, -50)));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _timer -= gameTime.ElapsedGameTime.TotalSeconds;
        if(_timer <= 0)
            GetScene().RemoveEntity(this);
    }
}