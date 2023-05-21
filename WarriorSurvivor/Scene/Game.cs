using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using SharpEngine.Widgets;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Widget;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public Player Player = null!;
    public readonly List<Enemy> Enemies = new();
    public readonly List<ExpPoint> ExpPoints = new();
    public readonly List<Chest> Chests = new();

    private readonly Label _goldLabel;
    
    public Game()
    {
        AddWidget(new WeaponDisplayer(new Vec2(50, 60)));
        _goldLabel = AddWidget(new Label(new Vec2(85, 130), "Or : 0", "small"));
        _goldLabel.ZLayer = 4095;
        Init();
    }

    public void Init()
    {
        RemoveAllEntities();
        
        AddEntity(new Map()).Initialize();
        Player = AddEntity(new Player());
        Player.Initialize();
        CameraManager.FollowEntity = Player;

        for (var i = 0; i < 10; i++)
            AddExpPoint(new ExpPoint(Player.GetComponent<TransformComponent>().Position + new Vec2(50 + 50 * i),
                1));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _goldLabel.Text = $"Or : {WS.PlayerData.Gold}";
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