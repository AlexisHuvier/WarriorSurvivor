using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Physic;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using WarriorSurvivor.Data;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Entity;

public class ActiveWeapon: SharpEngine.Entities.Entity
{
    public float MovingValue;
    public WeaponData Data;
    
    private readonly TransformComponent _transform;
    private readonly SpriteComponent _sprite;
    private readonly PhysicsComponent _physics;
    private Vec2 _distancePos = Vec2.Zero;
    
    public ActiveWeapon()
    {
        _transform = AddComponent(new TransformComponent(zLayer: 12));
        _sprite = AddComponent(new SpriteComponent(""));
        _physics = AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true));
        _physics.AddRectangleCollision(new Vec2(30), tag: FixtureTag.IgnoreCollisions);
        _physics.CollisionCallback = CollisionCallback;
    }

    public void Init(string sprite, float scale, WeaponData data)
    {
        _transform.Scale = new Vec2(scale);
        _sprite.Sprite = sprite;
        Data = data;
    }

    private bool CollisionCallback(Fixture self, Fixture other, Contact contact)
    {
        if (_sprite.Sprite == "" || MovingValue < 0.1f) return false;

        if (GetScene<Game>().Enemies.FirstOrDefault(e => e.GetComponent<PhysicsComponent>().Body == other.Body) is
            { } enemy)
        {
            if(enemy.TakeDamage(Data.Stats.Attack + WS.PlayerData.Stats.Attack + WS.PlayerData.GetPassiveStats().Attack))
                SoundManager.Play("enemy-hit");
        }

        return false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var playerPosition = GetScene<Game>().Player.GetComponent<TransformComponent>().Position;
        var mousePosition = InputManager.GetMousePosition();
        var direction = new Vec2(
            mousePosition.X - playerPosition.X + CameraManager.Position.X,
            mousePosition.Y - playerPosition.Y + CameraManager.Position.Y
            ).Normalized;
        
        _physics.SetRotation((int)MathUtils.ToDegrees(MathF.Atan2(direction.Y, direction.X)));
        _physics.SetPosition(new Vec2(playerPosition.X + direction.X * 50, playerPosition.Y + direction.Y * 50));
        MovingValue = new Vec2(direction.X * 50 - _distancePos.X, direction.Y * 50 - _distancePos.Y).Length;
        _distancePos = direction * 50;
        _sprite.FlipY = direction.X < 0;
    }
}