using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class SpawnerComponent: SharpEngine.Components.Component
{
    private double _enemyTimer;
    private double _chestTimer;

    public double GetEnemyTimer()
    {
        var currentPlayTime = GetEntity().GetScene<Game>().GetPlayTime();
        if(currentPlayTime <= 300)
            return -1.95 * currentPlayTime  / 300 + 2;
        return 0.05;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _enemyTimer += gameTime.ElapsedGameTime.TotalSeconds;
        _chestTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_chestTimer > 2)
        {
            _chestTimer = 0;
            var corners = ((Map)GetEntity()).GetCorners();

            var position = Rand.GetRand(4) switch
            {
                0 => new Vec2(corners[0].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                1 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[0].Y),
                2 => new Vec2(corners[1].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                3 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[2].Y),
                _ => Vec2.Zero
            };

            GetEntity().GetScene<Game>().AddChest(new Chest(position));
        }
        
        if (_enemyTimer >= GetEnemyTimer() && GetEntity().GetScene<Game>().Enemies.Count <= WS.EnemyCount && GetEntity().GetScene<Game>().Boss == null)
        {
            _enemyTimer = 0;
            var corners = ((Map)GetEntity()).GetCorners();

            var position = Rand.GetRand(4) switch
            {
                0 => new Vec2(corners[0].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                1 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[0].Y),
                2 => new Vec2(corners[1].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                3 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[2].Y),
                _ => Vec2.Zero
            };

            GetEntity().GetScene<Game>().AddEnemy(new Enemy(position, new EnemyData
            {
                Stats = new Stats
                {
                    Level = 1,
                    Attack = 1,
                    Life = 10,
                    Speed = 350
                },
                Life = 10,
                Sprite = "enemy"
            }));
        }
    }
}