using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;

namespace WarriorSurvivor.Entity;

public class Player: SharpEngine.Entities.Entity
{
    private double _invincibility;
    
    public Player()
    {
        AddComponent(new TransformComponent(new Vec2(600, 450), new Vec2(3), zLayer: 10));
        AddComponent(new AnimSpriteSheetComponent("player", new Vec2(16, 28), new List<Animation>
        {
            new("die", new List<uint> { 0 }, 100f),
            new("idle", new List<uint> { 1, 2 }, 250f),
            new("walk", new List<uint> { 3, 4, 5, 6, 7, 8 }, 100f)
        }, "idle"));
        AddComponent(new ControlComponent(ControlType.FourDirection));
        AddComponent(new LifeBarComponent());
        AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(new Vec2(50));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_invincibility > 0)
            _invincibility -= gameTime.ElapsedGameTime.TotalSeconds;
        if (_invincibility < 0)
            _invincibility = 0;
        
        
        var control = GetComponent<ControlComponent>();
        var anim = GetComponent<AnimSpriteSheetComponent>();

        anim.FlipX = control.Direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => anim.FlipX
        };

        switch (control.IsMoving)
        {
            case true when anim.Anim == "idle":
                anim.Anim = "walk";
                break;
            case false when anim.Anim == "walk":
                anim.Anim = "idle";
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (_invincibility <= 0)
        {
            WS.PlayerData.Life -= damage;
            GetComponent<LifeBarComponent>().Value = (float)WS.PlayerData.Life * 100 / WS.PlayerData.Stats.Life;

            if (WS.PlayerData.Life == 0)
            {
                WS.PlayerData.Reset();
                GetScene().GetWindow().IndexCurrentScene = 2;
            }

            _invincibility = 0.1;
        }
    }
}