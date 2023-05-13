using SharpEngine.Components;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Entity;

public class Player: SharpEngine.Entities.Entity
{
    public Player()
    {
        AddComponent(new TransformComponent(new Vec2(600, 450)));
        AddComponent(new SpriteComponent("player"));
        AddComponent(new ControlComponent(ControlType.FourDirection));
    }
}