using SharpEngine.Components;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public Player Player = null!;
    public readonly List<ExpPoint> ExpPoints = new();
    public readonly List<Chest> Chests = new();
    
    public Game()
    {
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
        {
            ExpPoints.Add(AddEntity(new ExpPoint(Player.GetComponent<TransformComponent>().Position + new Vec2(50 + 50 * i),
                1)));
            ExpPoints[^1].Initialize();
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