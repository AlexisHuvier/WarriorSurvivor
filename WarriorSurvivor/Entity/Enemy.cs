using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class Enemy: SharpEngine.Entities.Entity
{
    public EnemyData Data;

    public Enemy(Vec2 position, EnemyData data)
    {
        Data = data;

        AddComponent(new TransformComponent(position));
        AddComponent(new SpriteComponent(data.Sprite));
        AddComponent(new EnemyMoverComponent(data));
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
}