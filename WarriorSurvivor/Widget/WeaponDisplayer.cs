using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using WarriorSurvivor.Data.DB;

namespace WarriorSurvivor.Widget;

public class WeaponDisplayer: SharpEngine.Widgets.Widget
{
    private static readonly Vec2 Size = new(48);
    
    public WeaponDisplayer(Vec2 position): base(position) {}

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        var textureManager = GetWindow().TextureManager;
        var blankTexture = textureManager.GetTexture("blank");
        var nullTexture = textureManager.GetTexture("weapon-null");

        Renderer.RenderTexture(GetWindow(),
            WS.PlayerData.ActiveWeapon.HasValue
                ? textureManager.GetTexture(Weapon.Types[WS.PlayerData.ActiveWeapon.Value.Name].Icon)
                : nullTexture, new Rect(Position, Size), Color.White, 1);
        Renderer.RenderTexture(GetWindow(), blankTexture, new Rect(Position.X + 51, Position.Y, 5, 48), Color.Black, 1);
        var nb = 0;
        foreach (var passiveWeapon in WS.PlayerData.PassiveWeapons)
        {
            Renderer.RenderTexture(GetWindow(),
                passiveWeapon.HasValue
                    ? textureManager.GetTexture(Weapon.Types[passiveWeapon.Value.Name].Icon)
                    : nullTexture, new Rect(59 + Position.X + 52 * nb, Position.Y, Size), Color.White, 1);
            nb++;
        }
    }
}