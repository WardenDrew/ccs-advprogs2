using CCS.AdvProgS2.Shared.Contract;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class WallEntity : IEntity
{
    public EntityTypes EntityType => EntityTypes.Wall;

    public int XCoord { get; set; }

    public int YCoord { get; set; }

    public ConsoleColor Color { get; set; }

    public char Symbol => 'W';
}