using SharpEngine.Components;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Component;

namespace WarriorSurvivor.Entity;

public class Player: SharpEngine.Entities.Entity
{
    private double _invincibility;
    
    public Player()
    {
        AddComponent(new TransformComponent(new Vec2(600, 450), zLayer: 10));
        AddComponent(new SpriteComponent("player"));
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