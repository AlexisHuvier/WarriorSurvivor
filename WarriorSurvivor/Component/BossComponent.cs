using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class BossComponent: SharpEngine.Components.Component
{
    private bool _attack;
    private int _speed;
    private bool _up = true;
    private double _timer = Rand.GetRandF(2, 4);
    private TransformComponent _transformComponent = null!;
    private PhysicsComponent _physicsComponent = null!;
    private AnimSpriteSheetComponent _animSpriteSheetComponent = null!;

    public override void Initialize()
    {
        base.Initialize();

        _speed = ((Boss)GetEntity()).Data.Stats.Speed;
        _transformComponent = GetEntity().GetComponent<TransformComponent>();
        _physicsComponent = GetEntity().GetComponent<PhysicsComponent>();
        _animSpriteSheetComponent = GetEntity().GetComponent<AnimSpriteSheetComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_attack)
        {
            _physicsComponent.SetLinearVelocity(Vec2.Zero);
            _animSpriteSheetComponent.Anim = "idle";
            if (_animSpriteSheetComponent.Offset.Y >= 0)
            {
                _animSpriteSheetComponent.Offset.Y = 0;
                var position = GetEntity().GetScene<Game>().Player.GetComponent<TransformComponent>().Position;
                var direction = (position - _physicsComponent.GetPosition()).Normalized;

                _up = true;
                _attack = false;
                _timer = Rand.GetRandF(2, 4);
                GetEntity().GetScene().AddEntity(new RockThrowed(_transformComponent.Position, direction * 200)).Initialize();
            }
            else if(_up)
            {
                _animSpriteSheetComponent.Offset.Y -= 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_animSpriteSheetComponent.Offset.Y <= -50)
                    _up = false;
            }
            else
                _animSpriteSheetComponent.Offset.Y += 300 * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
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

            _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer <= 0)
            {
                _attack = true;
                _animSpriteSheetComponent.Offset.Y = -1;
            }
        }
    }
}