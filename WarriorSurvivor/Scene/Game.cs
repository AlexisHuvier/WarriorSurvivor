using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Widget;
using Timer = WarriorSurvivor.Widget.Timer;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public Player Player = null!;
    public readonly List<Enemy> Enemies = new();
    public readonly List<ExpPoint> ExpPoints = new();
    public readonly List<Chest> Chests = new();
    
    private SharpEngine.Entities.Entity? _activeWeapon;
    private readonly SharpEngine.Entities.Entity?[] _passiveWeapons = { null, null, null, null, null };
    private readonly GoldDisplayer _goldLabel;
    private readonly Timer _timer;
    
    public Game()
    {
        AddWidget(new WeaponDisplayer(new Vec2(50, 60)));
        _timer = AddWidget(new Timer(new Vec2(60, 110)));
        _goldLabel = AddWidget(new GoldDisplayer(new Vec2(60, 140)));
        _goldLabel.ZLayer = 4095;
        Init();
    }

    public void Init()
    {
        RemoveAllEntities();
        
        _timer.Reset();

        AddEntity(new Map()).Initialize();
        Player = AddEntity(new Player());
        Player.Initialize();
        CameraManager.FollowEntity = Player;

        _activeWeapon = null;
        for (var i = 0; i < 5; i++)
            _passiveWeapons[i] = null;

        for (var i = 0; i < 10; i++)
            AddExpPoint(new ExpPoint(Player.GetComponent<TransformComponent>().Position + new Vec2(50 + 50 * i),
                1));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _goldLabel.Text = $"Or : {WS.PlayerData.Gold}";
    }

    public double GetPlayTime() => _timer.Time;

    public Vec2? GetNearestEnemyPosition(int index = 0)
    {
        var enemy = new List<Enemy>(Enemies);
        if (enemy.Count <= index)
            return null;
        enemy.Sort((enemy1, enemy2) =>
        {
            var playerPosition = Player.GetComponent<TransformComponent>().Position;
            var enemy1Distance = (enemy1.GetComponent<TransformComponent>().Position - playerPosition).LengthSquared;
            var enemy2Distance = (enemy2.GetComponent<TransformComponent>().Position - playerPosition).LengthSquared;
            return enemy1Distance.CompareTo(enemy2Distance);
        });
        return enemy[index].GetComponent<TransformComponent>().Position;
    }

    public void SetActiveWeapon(SharpEngine.Entities.Entity? activeWeapon)
    {
        if(_activeWeapon is not null)
            RemoveEntity(_activeWeapon, true);

        if (activeWeapon is not null)
        {
            _activeWeapon = activeWeapon;
            AddEntity(_activeWeapon);
            _activeWeapon.Initialize();
        }
        else
            _activeWeapon = null;
    }

    public void SetPassiveWeapon(SharpEngine.Entities.Entity? passiveWeapon, int index)
    {
        if(_passiveWeapons[index] is not null)
            RemoveEntity(_passiveWeapons[index], true);

        if (passiveWeapon is not null)
        {
            _passiveWeapons[index] = passiveWeapon;
            AddEntity(_passiveWeapons[index]);
            _passiveWeapons[index]!.Initialize();
        }
        else
            _passiveWeapons[index] = null;
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(AddEntity(enemy));
        enemy.Initialize();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        RemoveEntity(enemy, true);
        Enemies.Remove(enemy);
    }
    
    public void AddExpPoint(ExpPoint point)
    {
        ExpPoints.Add(AddEntity(point));
        point.Initialize();
    }

    public void RemoveExpPoint(ExpPoint point)
    {
        RemoveEntity(point, true);
        ExpPoints.Remove(point);
    }

    public void AddChest(Chest chest)
    {
        Chests.Add(AddEntity(chest));
        chest.Initialize();
    }

    public void RemoveChest(Chest chest)
    {
        RemoveEntity(chest, true);
        Chests.Remove(chest);
    }

    public Vec2[] GetCameraCorners()
    {
        var corners = new Vec2[4];

        corners[0] = CameraManager.Position;
        corners[1] = CameraManager.Position + new Vec2(1200, 0);
        corners[2] = CameraManager.Position + new Vec2(1200, 900);
        corners[3] = CameraManager.Position + new Vec2(0, 900);

        return corners;
    }
}