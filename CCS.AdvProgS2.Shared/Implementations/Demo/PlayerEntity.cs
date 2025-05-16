using CCS.AdvProgS2.Shared.Contract;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class PlayerEntity : IEntity
{
    public EntityTypes EntityType => EntityTypes.Player;

    public int XCoord { get; set; }

    public int YCoord { get; set; }

    public ConsoleColor Color { get; set; }

    public char Symbol => 'P';
}