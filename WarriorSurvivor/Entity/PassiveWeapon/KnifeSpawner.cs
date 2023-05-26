using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity.PassiveWeapon;

public class KnifeSpawner: SharpEngine.Entities.Entity
{
    private double _timer;
    private readonly WeaponData _data;
    
    public KnifeSpawner(WeaponData data)
    {
        _data = data;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= 1)
        {
            _timer = 0;
            for (var i = 0; i < _data.Stats.Level; i++)
            {
                var enemyPosition = GetScene<Game>().GetNearestEnemyPosition(i);
                if (enemyPosition == null)
                    break;
                var playerPosition = GetScene<Game>().Player.GetComponent<TransformComponent>().Position;
                var direction = (enemyPosition.Value - playerPosition).Normalized * 600;
                GetScene().AddEntity(new Knife(playerPosition, direction, 2 + _data.Stats.Level - 1)).Initialize();
            }
        }
    }
}