using SharpEngine.Components;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Component;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class Enemy: SharpEngine.Entities.Entity
{
    public EnemyData Data;
    
    private double _invincibility;

    public Enemy(Vec2 position, EnemyData data)
    {
        Data = data;

        AddComponent(new TransformComponent(position, new Vec2(3), zLayer: 10));
        AddComponent(new AnimSpriteSheetComponent(data.Sprite, new Vec2(18, 25), new List<Animation>
        {
            new("idle", new List<uint> { 0, 1, 2, 3 }, 250f),
            new("walk", new List<uint> { 4, 5, 6, 7 }, 100f)
        }, "idle"));
        AddComponent(new EnemyMoverComponent(data));
        AddComponent(new LifeBarComponent());
        var phys = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        phys.AddRectangleCollision(new Vec2(35, 40));
        phys.CollisionCallback = PhysCollisionCallback;
    }

    private bool PhysCollisionCallback(Fixture fixture, Fixture other, Contact contact)
    {
        var player = ((Game)GetScene()).Player;
        if (other.Body == player.GetComponent<PhysicsComponent>().Body) player.TakeDamage(Data.Stats.Attack);

        return true;
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
            Data.Life -= damage;
            GetComponent<LifeBarComponent>().Value = (float)Data.Life * 100 / Data.Stats.Life;

            if (Data.Life <= 0)
            {
                WS.PlayerData.ModifyGold(-1);
                GetScene<Game>().AddExpPoint(new ExpPoint(GetComponent<TransformComponent>().Position, Data.Stats.Level));
                GetScene<Game>().RemoveEnemy(this);
            }

            _invincibility = 0.1;
        }
    }
}