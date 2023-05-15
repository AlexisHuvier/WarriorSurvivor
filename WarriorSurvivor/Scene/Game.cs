using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public readonly Player Player;
    
    public Game()
    {
        AddEntity(new Map());
        Player = AddEntity(new Player());
        
        CameraManager.FollowEntity = Player;
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