using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Data;
using Color = SharpEngine.Utils.Color;
using Game = WarriorSurvivor.Scene.Game;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace WarriorSurvivor.Entity.Weapon;

public class FireCircle: SharpEngine.Entities.Entity
{
    private WeaponData _data;
    private float _rotation;

    public FireCircle(WeaponData data)
    {
        _data = data;

        AddComponent(new TransformComponent(Vec2.Zero));
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(30), tag: FixtureTag.IgnoreCollisions);
        phys.CollisionCallback = PhysCollisionCallback;
        
        var particles = AddComponent(new ParticleComponent());
        
        particles.AddEmitter(new ParticleEmitter(
                new []{Color.Yellow},
                new []{Color.Red},
                minSize: 20, maxSize: 20, sizeFunction: ParticleParametersFunction.Decrease,
                minTimerBeforeSpawn: 0.1f, maxTimerBeforeSpawn: 0.1f,
                minLifetime: 0.3f, maxLifetime: 0.3f,
                minVelocity: 0f, maxVelocity: 0f,
                spawnSize: new Vec2(15), 
                active: true
            )
        );
    }

    private bool PhysCollisionCallback(Fixture fixture, Fixture other, Contact contact)
    {
        if (GetScene<Game>().Enemies.FirstOrDefault(e => e.GetComponent<PhysicsComponent>().Body == other.Body) is { } enemy) 
            enemy.TakeDamage(2 * _data.Stats.Level);
        return false;
    }

    private Vec2 CalculatePosition()
    {
        var playerPos = GetScene<Game>().Player.GetComponent<PhysicsComponent>().GetPosition();

        return new Vec2(
            100 * MathF.Cos(MathUtils.ToRadians(_rotation)) + playerPos.X,
            100 * MathF.Sin(MathUtils.ToRadians(_rotation)) + playerPos.Y
        );
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 200 * _data.Stats.Level;
        GetComponent<PhysicsComponent>().SetPosition(CalculatePosition());
    }
}