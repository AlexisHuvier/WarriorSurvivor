using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace WarriorSurvivor.Widget;

public class Timer: SharpEngine.Widgets.Widget
{
    public double Time;
    
    public Timer(Vec2 position) : base(position)
    { }

    public void Reset()
    {
        Time = 0;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Time += gameTime.ElapsedGameTime.TotalSeconds;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        Renderer.RenderText(GetWindow(), GetWindow().FontManager.GetFont("small"),
            $"Time : {(int)Time / 60} min {(int)Time % 60} sec {(int)(Time % 1 * 1000)}", GetRealPosition(), Color.Black,
            1);
    }
}