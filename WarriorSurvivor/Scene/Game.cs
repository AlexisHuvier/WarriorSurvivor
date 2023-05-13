using SharpEngine.Managers;
using WarriorSurvivor.Entity;

namespace WarriorSurvivor.Scene;

public class Game: SharpEngine.Scene
{
    public Game()
    {
        AddEntity(new Map());
        CameraManager.FollowEntity = AddEntity(new Player());
    }
}