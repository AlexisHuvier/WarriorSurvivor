using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class Boss: SharpEngine.Entities.Entity
{
    public readonly EnemyData Data;
    
    private double _invincibility;
    
    private readonly AnimSpriteSheetComponent _animSpriteSheetComponent;
    private readonly LifeBarComponent _lifeBarComponent;
    private readonly TransformComponent _transformComponent;
    
    public Boss(Vec2 position)
    {
        Data = new EnemyData
        {
            Life = 100,
            Sprite = "",
            Stats = new Stats
            {
                Attack = 4,
                Level = 10,
                Life = 100,
                Speed = 150
            }
        };
        
        _transformComponent = AddComponent(new TransformComponent(position, new Vec2(3), zLayer: 10));
        _lifeBarComponent = AddComponent(new LifeBarComponent(new Vec2(0, 70)));
        AddComponent(new BossComponent());
        _animSpriteSheetComponent = AddComponent(new AnimSpriteSheetComponent("boss", new Vec2(32, 36),
            new List<Animation> {
                new("idle", new List<uint> { 0, 1, 2, 3 }, 250f),
                new("walk", new List<uint> { 4, 5, 6, 7 }, 100f)
            }, "idle"));
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(75));
        phys.CollisionCallback = CollisionCallback;
    }

    private bool CollisionCallback(Fixture arg1, Fixture other, Contact arg3)
    {
        var player = ((Game)GetScene()).Player;
        if (other.Body == player.GetComponent<PhysicsComponent>().Body) 
            player.TakeDamage(Data.Stats.Attack);

        return true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (InputManager.IsKeyDown(Key.A))
            _animSpriteSheetComponent.Anim = "walk";

        if (_invincibility > 0)
            _invincibility -= gameTime.ElapsedGameTime.TotalSeconds;
        if (_invincibility < 0)
            _invincibility = 0;
    }

    public bool TakeDamage(int damage)
    {
        if (_invincibility > 0 && damage >= 0) return false;
        
        Data.Life -= damage;
        _lifeBarComponent.Value = (float)Data.Life * 100 / Data.Stats.Life;

        if (Data.Life <= 0)
        {
            WS.PlayerData.ModifyGold(-5);
            GetScene<Game>().AddExpPoint(new ExpPoint(_transformComponent.Position, Data.Stats.Level));
            GetScene<Game>().BeatBoss();
        }

        switch (damage)
        {
            case > 0:
                _invincibility = 0.1;
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