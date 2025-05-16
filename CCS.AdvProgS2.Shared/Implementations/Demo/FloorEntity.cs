using CCS.AdvProgS2.Shared.Contract;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class FloorEntity : IEntity
{
    public EntityTypes EntityType => EntityTypes.Floor;

    public int XCoord { get; set; }

    public int YCoord { get; set; }

    public ConsoleColor Color { get; set; }

    public char Symbol => ' ';
}