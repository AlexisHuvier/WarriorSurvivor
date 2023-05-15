using WarriorSurvivor.Data;
using WarriorSurvivor.Manager;

namespace WarriorSurvivor;

// ReSharper disable once InconsistentNaming
public static class WS
{
    public static readonly SaveManager SaveManager = new();

    internal static PlayerData PlayerData = new();
}