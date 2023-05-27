using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data;
using WarriorSurvivor.Data.DB;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Widget;
using Timer = WarriorSurvivor.Widget.Timer;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public Player Player = null!;
    public Boss? Boss = null;
    public readonly List<Enemy> Enemies = new();
    public readonly List<ExpPoint> ExpPoints = new();
    public readonly List<Chest> Chests = new();
    public readonly List<Rock> Rocks = new();
    public ActiveWeapon ActiveWeapon = null!;
    
    private readonly SharpEngine.Entities.Entity?[] _passiveWeapons = { null, null, null, null, null };
    private readonly GoldDisplayer _goldLabel;
    private readonly GainLevelDisplayer _gainLevelDisplayer;
    private readonly ChestDisplayer _chestDisplayer;
    private readonly Timer _timer;
    
    public Game()
    {
        AddWidget(new WeaponDisplayer(new Vec2(50, 60)));
        _timer = AddWidget(new Timer(new Vec2(60, 110)));
        _goldLabel = AddWidget(new GoldDisplayer(new Vec2(60, 140)));
        _gainLevelDisplayer = AddWidget(new GainLevelDisplayer());
        _chestDisplayer = AddWidget(new ChestDisplayer());
        Init(false);
    }

    public void Init(bool playSound = true)
    {
        RemoveAllEntities();
        
        _timer.Reset();

        AddEntity(new Map()).Initialize();
        Player = AddEntity(new Player());
        Player.Initialize();
        WS.OptionsManager.ApplyControls(Player);
        CameraManager.FollowEntity = Player;

        ActiveWeapon = AddEntity(new ActiveWeapon());
        ActiveWeapon.Initialize();
        for (var i = 0; i < 5; i++)
            _passiveWeapons[i] = null;
        
        SetActiveWeapon(Weapon.ActiveWeapons["Couteau"], playSound);
        
        SpawnBoss();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _goldLabel.Text = $"Or : {WS.PlayerData.Gold}";
    }

    public void SpawnBoss()
    {
        foreach (var enemy in Enemies)
            RemoveEntity(enemy, true);
        Enemies.Clear();

        var playerPosition = Player.GetComponent<TransformComponent>().Position;
        for (var y = -500; y <= 500; y += 50)
        {
            var rock1 = AddEntity(new Rock(new Vec2(playerPosition.X - 500, playerPosition.Y + y)));
            rock1.Initialize();
            Rocks.Add(rock1);
            var rock2 = AddEntity(new Rock(new Vec2(playerPosition.X + 500, playerPosition.Y + y)));
            rock2.Initialize();
            Rocks.Add(rock2);
        }
        
        for (var x = -450; x < 500; x += 50)
        {
            var rock1 = AddEntity(new Rock(new Vec2(playerPosition.X + x, playerPosition.Y - 500)));
            rock1.Initialize();
            Rocks.Add(rock1);
            var rock2 = AddEntity(new Rock(new Vec2(playerPosition.X + x, playerPosition.Y + 500)));
            rock2.Initialize();
            Rocks.Add(rock2);
        }

        var offset = Rand.GetRand(200, 400);
        Boss = Rand.GetRand(4) switch
        {
            0 => AddEntity(new Boss(new Vec2(playerPosition.X - offset, playerPosition.Y - offset))),
            1 => AddEntity(new Boss(new Vec2(playerPosition.X - offset, playerPosition.Y + offset))),
            2 => AddEntity(new Boss(new Vec2(playerPosition.X + offset, playerPosition.Y + offset))),
            3 => AddEntity(new Boss(new Vec2(playerPosition.X + offset, playerPosition.Y - offset))),
            _ => Boss
        };
        Boss?.Initialize();
    }

    public double GetPlayTime() => _timer.Time;

    public void GainLevel()
    {
        _gainLevelDisplayer.Displayed = true;
        _gainLevelDisplayer.Reset();
        Paused = true;
    }

    public void OpenChest()
    {
        _chestDisplayer.Displayed = true;
        _chestDisplayer.Reset();
        Paused = true;
        SoundManager.Play("chest");
    }

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

    public void SetActiveWeapon(Weapon? weapon, bool playSound = true)
    {
        if(weapon == null)
            ActiveWeapon.Init("", 1, new WeaponData());
        else
        {
            if(playSound)
                SoundManager.Play("equip");
            ActiveWeapon.Init(weapon.Icon, weapon.Scale,
                weapon.Name == ActiveWeapon.Data.Name
                    ? new WeaponData(weapon.Name, weapon.BaseStats.Multiply(ActiveWeapon.Data.Stats.Level + 1))
                    : new WeaponData(weapon.Name, weapon.BaseStats));
        }
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
        
        Player.TakeDamage(0);
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
        SoundManager.Play("exp_point-pop");
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