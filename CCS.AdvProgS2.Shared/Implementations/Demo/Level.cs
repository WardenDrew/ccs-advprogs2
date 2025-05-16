using CCS.AdvProgS2.Shared.Contract;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class Level : ILevel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<IEntity> Entities { get; set; } = [];

    public int Width { get; set; }

    public int Height { get; set; }
}