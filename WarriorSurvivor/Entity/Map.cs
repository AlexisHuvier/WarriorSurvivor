using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;

namespace WarriorSurvivor.Entity;

public class Map: SharpEngine.Entities.Entity
{
    private static readonly Vec2 SpriteOffset = new(640, 480);
    private readonly Vec2[] _corners = new Vec2[4];

    public Map()
    {
        AddComponent(new TransformComponent(new Vec2(600, 450)));
        AddComponent(new SpriteComponent("bg", true, SpriteOffset));
        AddComponent(new SpriteComponent("bg", true, -SpriteOffset));
        AddComponent(new SpriteComponent("bg", true, new Vec2(SpriteOffset.X, -SpriteOffset.Y)));
        AddComponent(new SpriteComponent("bg", true, new Vec2(-SpriteOffset.X, SpriteOffset.Y)));
        AddComponent(new MapMoverComponent());
        CalculateCorners();
    }

    public void CalculateCorners()
    {
        var position = GetComponent<TransformComponent>().Position;

        _corners[0] = position - SpriteOffset * 2;
        _corners[1] = position + new Vec2(SpriteOffset.X * 2, -SpriteOffset.Y * 2);
        _corners[2] = position + SpriteOffset * 2;
        _corners[3] = position + new Vec2(-SpriteOffset.X * 2, SpriteOffset.Y * 2);
    }

    public Vec2[] GetCorners() => _corners;
}