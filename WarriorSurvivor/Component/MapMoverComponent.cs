using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class MapMoverComponent: SharpEngine.Components.Component
{
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var transform = GetEntity().GetComponent<TransformComponent>();
        var corners = ((Map)GetEntity()).GetCorners();
        var cameraCorners = GetEntity().GetScene<Game>().GetCameraCorners();
        var temp = transform.Position;

        if (cameraCorners[0].X <= corners[0].X && cameraCorners[0].Y <= corners[0].Y)
            transform.Position = corners[0];
        else if (cameraCorners[1].X >= corners[1].X && cameraCorners[1].Y <= corners[1].Y)
            transform.Position = corners[1];
        else if (cameraCorners[2].X >= corners[2].X && cameraCorners[2].Y >= corners[2].Y)
            transform.Position = corners[2];
        else if (cameraCorners[3].X <= corners[0].X && cameraCorners[3].Y >= corners[3].Y)
            transform.Position = corners[3];
        else if (cameraCorners[0].Y <= corners[0].Y)
            transform.Position = new Vec2(transform.Position.X, corners[0].Y);
        else if (cameraCorners[1].X >= corners[1].X)
            transform.Position = new Vec2(corners[1].X, transform.Position.Y);
        else if (cameraCorners[2].Y >= corners[2].Y)
            transform.Position = new Vec2(transform.Position.X, corners[2].Y);
        else if (cameraCorners[3].X <= corners[3].X)
            transform.Position = new Vec2(corners[3].X, transform.Position.Y);
        
        if(temp != transform.Position)
            ((Map)GetEntity()).CalculateCorners();
    }
}