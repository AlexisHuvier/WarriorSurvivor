using SharpEngine.Components;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Entity;
using WarriorSurvivor.Scene;

namespace WarriorSurvivor.Component;

public class MapMoverComponent: SharpEngine.Components.Component
{
    private TransformComponent _transformComponent = null!;

    public override void Initialize()
    {
        base.Initialize();
        _transformComponent = GetEntity().GetComponent<TransformComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var corners = ((Map)GetEntity()).GetCorners();
        var cameraCorners = GetEntity().GetScene<Game>().GetCameraCorners();
        var temp = _transformComponent.Position;

        if (cameraCorners[0].X <= corners[0].X && cameraCorners[0].Y <= corners[0].Y)
            _transformComponent.Position = corners[0];
        else if (cameraCorners[1].X >= corners[1].X && cameraCorners[1].Y <= corners[1].Y)
            _transformComponent.Position = corners[1];
        else if (cameraCorners[2].X >= corners[2].X && cameraCorners[2].Y >= corners[2].Y)
            _transformComponent.Position = corners[2];
        else if (cameraCorners[3].X <= corners[0].X && cameraCorners[3].Y >= corners[3].Y)
            _transformComponent.Position = corners[3];
        else if (cameraCorners[0].Y <= corners[0].Y)
            _transformComponent.Position = new Vec2(_transformComponent.Position.X, corners[0].Y);
        else if (cameraCorners[1].X >= corners[1].X)
            _transformComponent.Position = new Vec2(corners[1].X, _transformComponent.Position.Y);
        else if (cameraCorners[2].Y >= corners[2].Y)
            _transformComponent.Position = new Vec2(_transformComponent.Position.X, corners[2].Y);
        else if (cameraCorners[3].X <= corners[3].X)
            _transformComponent.Position = new Vec2(corners[3].X, _transformComponent.Position.Y);
        
        if(temp != _transformComponent.Position)
            ((Map)GetEntity()).CalculateCorners();
    }
}