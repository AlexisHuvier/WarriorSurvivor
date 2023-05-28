using SharpEngine.Components;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class RockThrowed: SharpEngine.Entities.Entity
{
    public RockThrowed(Vec2 position, Vec2 direction)
    {
        AddComponent(new TransformComponent(position, zLayer: 11));
        AddComponent(new SpriteComponent("rock"));
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(30), tag: FixtureTag.IgnoreCollisions);
        phys.CollisionCallback = CollisionCallback;
        AddComponent(new AutoMovementComponent(direction));
    }

    private bool CollisionCallback(Fixture arg1, Fixture other, Contact arg3)
    {
        
        var player = ((Game)GetScene()).Player;
        if (other.Body == player.GetComponent<PhysicsComponent>().Body)
        {
            player.TakeDamage(5);
            GetScene().RemoveEntity(this, true);
        }
        else if(GetScene<Game>().Rocks.FirstOrDefault(c => c.GetComponent<PhysicsComponent>().Body == other.Body) is { } rock)
            GetScene().RemoveEntity(this, true);
        
        return false;
    }
}