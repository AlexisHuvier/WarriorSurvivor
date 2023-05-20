using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class EnemyMoverComponent: SharpEngine.Components.Component
{
    private readonly int _speed;

    public EnemyMoverComponent(EnemyData enemyData)
    {
        _speed = enemyData.Stats.Speed;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var physics = GetEntity().GetComponent<PhysicsComponent>();
        
        var position = GetEntity().GetScene<Game>().Player.GetComponent<TransformComponent>().Position;
        var direction = (position - physics.GetPosition()).Normalized();

        physics.SetLinearVelocity(direction * _speed);
        
        var anim = GetEntity().GetComponent<AnimSpriteSheetComponent>();

        anim.FlipX = direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => anim.FlipX
        };

        if (direction == Vec2.Zero && anim.Anim == "walk")
            anim.Anim = "idle";
        else if (direction != Vec2.Zero && anim.Anim == "idle")
            anim.Anim = "walk";
    }
}