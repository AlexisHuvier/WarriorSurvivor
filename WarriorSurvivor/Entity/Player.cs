using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Component;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class Player: SharpEngine.Entities.Entity
{
    private double _invincibility;
    private bool _playHitSound = true;

    private readonly ControlComponent _controlComponent;
    private readonly AnimSpriteSheetComponent _animSpriteSheetComponent;
    private readonly TransformComponent _transformComponent;
    private readonly LifeBarComponent _lifeBarComponent;

    public Player()
    {
        _transformComponent = AddComponent(new TransformComponent(new Vec2(600, 450), new Vec2(3), zLayer: 10));
        _animSpriteSheetComponent = AddComponent(new AnimSpriteSheetComponent("player", new Vec2(16, 28), new List<Animation>
        {
            new("die", new List<uint> { 0 }, 100f),
            new("idle", new List<uint> { 1, 2 }, 250f),
            new("walk", new List<uint> { 3, 4, 5, 6, 7, 8 }, 100f)
        }, "idle"));
        _controlComponent = AddComponent(new ControlComponent(ControlType.FourDirection));
        _lifeBarComponent = AddComponent(new LifeBarComponent(new Vec2(0, 60)));
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
        
        
        _controlComponent.Speed = WS.PlayerData.Stats.Speed + WS.PlayerData.GetPassiveStats().Speed;

        _animSpriteSheetComponent.FlipX = _controlComponent.Direction.X switch
        {
            < 0 => true,
            > 0 => false,
            _ => _animSpriteSheetComponent.FlipX
        };

        switch (_controlComponent.IsMoving)
        {
            case true when _animSpriteSheetComponent.Anim == "idle":
                _animSpriteSheetComponent.Anim = "walk";
                break;
            case false when _animSpriteSheetComponent.Anim == "walk":
                _animSpriteSheetComponent.Anim = "idle";
                break;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (_invincibility > 0 && damage >= 0) return false;
        
        WS.PlayerData.Life -= damage;
        
        var maxLife = WS.PlayerData.Stats.Life + WS.PlayerData.GetPassiveStats().Life;
        if (WS.PlayerData.Life > maxLife)
            WS.PlayerData.Life = maxLife;
        
        _lifeBarComponent.Value = (float)WS.PlayerData.Life * 100 / maxLife;

        if (WS.PlayerData.Life <= 0)
        {
            WS.PlayerData.Reset();
            GetScene().GetWindow().IndexCurrentScene = 2;
        }

        switch (damage)
        {
            case > 0:
                _invincibility = 0.1;
                if(_playHitSound)
                    SoundManager.Play("player-hit");
                _playHitSound = !_playHitSound;
                GetScene().AddEntity(new DamageDisplayer(_transformComponent.Position, Color.DarkRed,
                    damage.ToString())).Initialize();
                break;
            case < 0:
                GetScene().AddEntity(new DamageDisplayer(_transformComponent.Position, Color.Green,
                    damage.ToString())).Initialize();
                break;
        }

        return true;
    }
}