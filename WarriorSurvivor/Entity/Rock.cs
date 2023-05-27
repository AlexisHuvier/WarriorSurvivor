using SharpEngine.Components;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;

namespace WarriorSurvivor.Entity;

public class Rock: SharpEngine.Entities.Entity
{
    public Rock(Vec2 position)
    {
        AddComponent(new TransformComponent(position, new Vec2(1.5f), zLayer: 11));
        AddComponent(new SpriteComponent("rock"));
        AddComponent(new PhysicsComponent(BodyType.Static)).AddRectangleCollision(new Vec2(50));
    }
}