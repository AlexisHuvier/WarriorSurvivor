using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Entity;

namespace WarriorSurvivor.Component;

public class EnemySpawnerComponent: SharpEngine.Components.Component
{
    private double _timer;
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _timer += gameTime.ElapsedGameTime.TotalSeconds;
        if (_timer > 0.5)
        {
            _timer = 0;
            var corners = ((Map)GetEntity()).GetCorners();

            var position = Rand.GetRand(4) switch
            {
                0 => new Vec2(corners[0].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                1 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[0].Y),
                2 => new Vec2(corners[1].X, Rand.GetRandF(corners[0].Y, corners[3].Y)),
                3 => new Vec2(Rand.GetRandF(corners[0].X, corners[1].X), corners[2].Y),
                _ => Vec2.Zero
            };

            GetEntity().GetScene().AddEntity(new Enemy(position, new EnemyData
            {
                Stats = new Stats
                {
                    Attack = 1,
                    Life = 10,
                    Speed = 200
                },
                Life = 10,
                Sprite = "test"
            })).Initialize();
        }
    }
}