using SharpEngine.Components;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using WarriorSurvivor.Component;

namespace WarriorSurvivor.Entity;

public class ExpPoint: SharpEngine.Entities.Entity
{
    public int Value;
    
    public ExpPoint(Vec2 position, int value)
    {
        AddComponent(new TransformComponent(position, zLayer: 5));
        AddComponent(new ExpDisplayComponent());
        AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true))
            .AddRectangleCollision(new Vec2(20), tag: FixtureTag.IgnoreCollisions);

        Value = value;
    }
}