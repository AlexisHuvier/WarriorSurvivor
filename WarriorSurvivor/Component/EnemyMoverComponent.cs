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

        var transform = GetEntity().GetComponent<TransformComponent>();
        
        var position = ((Game)GetEntity().GetScene()).Player.GetComponent<TransformComponent>().Position;
        var direction = (position - transform.Position).Normalized();

        transform.Position += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}