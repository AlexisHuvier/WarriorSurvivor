using SharpEngine.Components;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Entity;

public class Map: SharpEngine.Entities.Entity
{
    private static readonly Vec2 SpriteOffset = new(640, 480);
    
    private readonly List<SpriteComponent> _sprites = new();

    public Map()
    {
        AddComponent(new TransformComponent(new Vec2(600, 450)));
        _sprites.Add(AddComponent(new SpriteComponent("bg", true, SpriteOffset)));
        _sprites.Add(AddComponent(new SpriteComponent("bg", true, -SpriteOffset)));
        _sprites.Add(AddComponent(new SpriteComponent("bg", true, new Vec2(SpriteOffset.X, -SpriteOffset.Y))));
        _sprites.Add(AddComponent(new SpriteComponent("bg", true, new Vec2(-SpriteOffset.X, SpriteOffset.Y))));
    }
}