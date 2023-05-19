using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class Enemy: SharpEngine.Entities.Entity
{
    public EnemyData Data;
    
    private double _invincibility;

    public Enemy(Vec2 position, EnemyData data)
    {
        Data = data;

        AddComponent(new TransformComponent(position, new Vec2(3), zLayer: 10));
        AddComponent(new AnimSpriteSheetComponent(data.Sprite, new Vec2(18, 25), new List<Animation>
        {
            new("idle", new List<uint> { 0, 1, 2, 3 }, 250f),
            new("walk", new List<uint> { 4, 5, 6, 7 }, 100f)
        }, "idle"));
        AddComponent(new EnemyMoverComponent(data));
        AddComponent(new LifeBarComponent());
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(35, 40));
        phys.CollisionCallback = (_, other, _) =>
        {
            var player = ((Game)GetScene()).Player;
            if (other.Body == player.GetComponent<PhysicsComponent>().Body)
                player.TakeDamage(data.Stats.Attack);

            return true;
        };
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_invincibility > 0)
            _invincibility -= gameTime.ElapsedGameTime.TotalSeconds;
        if (_invincibility < 0)
            _invincibility = 0;
    }

    public void TakeDamage(int damage)
    {
        if (_invincibility <= 0)
        {
            Data.Life -= damage;
            GetComponent<LifeBarComponent>().Value = (float)Data.Life * 100 / Data.Stats.Life;

            if (Data.Life == 0)
                GetScene().RemoveEntity(this);

            _invincibility = 0.1;
        }
    }
}