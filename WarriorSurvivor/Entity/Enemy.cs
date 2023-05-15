using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;

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
    }
}