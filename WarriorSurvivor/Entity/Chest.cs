using SharpEngine.Components;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;

namespace WarriorSurvivor.Entity;

public class Chest: SharpEngine.Entities.Entity
{
    public Chest(Vec2 position)
    {
        AddComponent(new TransformComponent(position, new Vec2(3), zLayer: 9));
        AddComponent(new SpriteComponent("chest"));
        AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true))
            .AddRectangleCollision(new Vec2(40), tag: FixtureTag.IgnoreCollisions);
    }
}