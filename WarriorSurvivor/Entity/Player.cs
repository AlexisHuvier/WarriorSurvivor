using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

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
        AddComponent(new ExpBarComponent());
        var physics = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        physics.AddRectangleCollision(new Vec2(35, 40));
        physics.CollisionCallback = PhysicsCollisionCallback;
    }

    private bool PhysicsCollisionCallback(Fixture fixture, Fixture other, Contact contact)
    {
        if (GetScene<Game>().ExpPoints.FirstOrDefault(e => e.GetComponent<PhysicsComponent>().Body == other.Body) is { } expPoint)
        {
            GetScene<Game>().RemoveExpPoint(expPoint);
            if (WS.PlayerData.AddExp(expPoint.Value))
                GetScene<Game>().GainLevel();

            return false;
        }

        if (GetScene<Game>().Chests.FirstOrDefault(c => c.GetComponent<PhysicsComponent>().Body == other.Body) is { } chest)
        {
            GetScene<Game>().RemoveChest(chest);
            GetScene<Game>().OpenChest();
            return false;
        }

        return true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_invincibility > 0)
            _invincibility -= gameTime.ElapsedGameTime.TotalSeconds;
        if (_invincibility < 0)
            _invincibility = 0;
        
        
        var control = GetComponent<ControlComponent>();
        control.Speed = WS.PlayerData.Stats.Speed + WS.PlayerData.GetPassiveStats().Speed;

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
        if (_invincibility <= 0 || damage < 0)
        {
            WS.PlayerData.Life -= damage;
            
            var maxLife = WS.PlayerData.Stats.Life + WS.PlayerData.GetPassiveStats().Life;
            if (WS.PlayerData.Life > maxLife)
                WS.PlayerData.Life = maxLife;
            
            GetComponent<LifeBarComponent>().Value = (float)WS.PlayerData.Life * 100 / maxLife;

            if (WS.PlayerData.Life <= 0)
            {
                WS.PlayerData.Reset();
                GetScene().GetWindow().IndexCurrentScene = 2;
            }

            switch (damage)
            {
                case > 0:
                    _invincibility = 0.1;
                    GetScene().AddEntity(new DamageDisplayer(GetComponent<TransformComponent>().Position, Color.DarkRed,
                        damage.ToString())).Initialize();
                    break;
                case < 0:
                    GetScene().AddEntity(new DamageDisplayer(GetComponent<TransformComponent>().Position, Color.Green,
                        damage.ToString())).Initialize();
                    break;
            }
        }
    }
}