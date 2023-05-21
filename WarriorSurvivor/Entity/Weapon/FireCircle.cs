using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
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
        phys.AddRectangleCollision(new Vec2(20), tag: FixtureTag.IgnoreCollisions);
        phys.CollisionCallback = (_, other, _) =>
        {
            if (GetScene<Game>().Enemies.FirstOrDefault(e => e.GetComponent<PhysicsComponent>().Body == other.Body) is
                { } enemy)
                enemy.TakeDamage(2 * _data.Stats.Level);

            return false;
        };
        
        var particles = AddComponent(new ParticleComponent());
        
        particles.AddEmitter(new ParticleEmitter(
                new []{Color.Yellow},
                new []{Color.Red},
                minSize: 10, maxSize: 10, sizeFunction: ParticleParametersFunction.Decrease,
                minTimerBeforeSpawn: 0.1f, maxTimerBeforeSpawn: 0.1f,
                minLifetime: 0.5f, maxLifetime: 0.5f,
                minVelocity: 0f, maxVelocity: 0f,
                spawnSize: new Vec2(15), 
                active: true
            )
        );
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