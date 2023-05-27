using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Entity;

public class Boss: SharpEngine.Entities.Entity
{
    private readonly AnimSpriteSheetComponent _animSpriteSheetComponent;
    
    public Boss(Vec2 position)
    {
        AddComponent(new TransformComponent(position, new Vec2(3), zLayer: 10));
        _animSpriteSheetComponent = AddComponent(new AnimSpriteSheetComponent("boss", new Vec2(32, 36),
            new List<Animation> {
                new("idle", new List<uint> { 0, 1, 2, 3 }, 250f),
                new("walk", new List<uint> { 4, 5, 6, 7 }, 100f)
            }, "idle"));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsKeyDown(Key.A))
            _animSpriteSheetComponent.Anim = "walk";
    }
}