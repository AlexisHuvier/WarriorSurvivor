using SharpEngine.Utils;

namespace WarriorSurvivor.Data;

public struct Stats
{

    public int Life;
    public int Speed;
    public int Attack;
    public int Defense;

    public override string ToString() => $"[Life: {Life}, Speed: {Speed}, Attack: {Attack}, Defense: {Defense}]";
}