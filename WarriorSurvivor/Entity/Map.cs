using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;

namespace WarriorSurvivor.Entity;

public class Map: SharpEngine.Entities.Entity
{
    private static readonly Vec2 SpriteOffset = new(640, 480);
    private readonly Vec2[] _corners = new Vec2[4];
    private readonly TransformComponent _transformComponent;

    public Map()
    {
        _transformComponent = AddComponent(new TransformComponent(new Vec2(600, 450)));
        AddComponent(new SpriteComponent("bg", true, SpriteOffset));
        AddComponent(new SpriteComponent("bg", true, -SpriteOffset));
        AddComponent(new SpriteComponent("bg", true, new Vec2(SpriteOffset.X, -SpriteOffset.Y)));
        AddComponent(new SpriteComponent("bg", true, new Vec2(-SpriteOffset.X, SpriteOffset.Y)));
        AddComponent(new MapMoverComponent());
        AddComponent(new SpawnerComponent());
        CalculateCorners();
    }

    public void CalculateCorners()
    {
        var position = _transformComponent.Position;

        _corners[0] = new Vec2(position.X - SpriteOffset.X * 2, position.Y - SpriteOffset.Y * 2);
        _corners[1] = new Vec2(position.X + SpriteOffset.X * 2, position.Y - SpriteOffset.Y * 2);
        _corners[2] = new Vec2(position.X + SpriteOffset.X * 2, position.Y + SpriteOffset.Y * 2);
        _corners[3] = new Vec2(position.X - SpriteOffset.X * 2, position.Y + SpriteOffset.Y * 2);
    }

    public Vec2[] GetCorners() => _corners;
}