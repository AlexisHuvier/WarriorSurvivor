using SharpEngine.Components;
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

        AddComponent(new TransformComponent(position, zLayer: 10));
        AddComponent(new SpriteComponent(data.Sprite));
        AddComponent(new EnemyMoverComponent(data));
        AddComponent(new LifeBarComponent());
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(50));
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