using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class EnemyMoverComponent: SharpEngine.Components.Component
{
    private readonly int _speed;
    private PhysicsComponent _physicsComponent = null!;
    private AnimSpriteSheetComponent _animSpriteSheetComponent = null!;

    public EnemyMoverComponent(EnemyData enemyData)
    {
        _speed = enemyData.Stats.Speed;
    }

    public override void Initialize()
    {
        base.Initialize();
        _physicsComponent = GetEntity().GetComponent<PhysicsComponent>();
        _animSpriteSheetComponent = GetEntity().GetComponent<AnimSpriteSheetComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        var position = GetEntity().GetScene<Game>().Player.GetComponent<TransformComponent>().Position;
        var direction = (position - _physicsComponent.GetPosition()).Normalized;

        _physicsComponent.SetLinearVelocity(direction * _speed);

        _animSpriteSheetComponent.FlipX = direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => _animSpriteSheetComponent.FlipX
        };

        if (direction == Vec2.Zero && _animSpriteSheetComponent.Anim == "walk")
            _animSpriteSheetComponent.Anim = "idle";
        else if (direction != Vec2.Zero && _animSpriteSheetComponent.Anim == "idle")
            _animSpriteSheetComponent.Anim = "walk";
    }
}