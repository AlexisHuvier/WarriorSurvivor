using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace WarriorSurvivor.Widget;

public class GoldDisplayer: SharpEngine.Widgets.Widget
{
    public string Text = "";

    public GoldDisplayer(Vec2 position) : base(position) {}

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        Renderer.RenderText(GetWindow(), GetWindow().FontManager.GetFont("small"), Text, GetRealPosition(), Color.Black, 1);
    }
}