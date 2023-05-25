using SharpEngine.Components;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity.PassiveWeapon;

public class Knife: SharpEngine.Entities.Entity
{
    private double _timer;
    private int _attack;
    
    public Knife(Vec2 position, Vec2 direction, int attack)
    {
        _attack = attack;
        
        AddComponent(new TransformComponent(position, new Vec2(0.8f), (int)MathUtils.ToDegrees(MathF.Atan2(direction.Y, direction.X)), 11));
        AddComponent(new AutoMovementComponent(direction));
        AddComponent(new SpriteComponent("weapon-couteau", flipY: direction.X < 0));
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(50), tag: FixtureTag.IgnoreCollisions);
        phys.CollisionCallback = CollisionCallback;
    }

    private bool CollisionCallback(Fixture self, Fixture other, Contact contact)
    {
        if (GetScene<Game>().Enemies.FirstOrDefault(e => e.GetComponent<PhysicsComponent>().Body == other.Body) is
            { } enemy)
        {
            enemy.TakeDamage(_attack);
            GetScene().RemoveEntity(this, true);
        }

        return false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _timer += gameTime.ElapsedGameTime.TotalSeconds;
        
        if(_timer >= 3)
            GetScene().RemoveEntity(this);
    }
}