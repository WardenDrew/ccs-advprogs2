using CCS.AdvProgS2.Shared.Contract;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class Player : IPlayer
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
}