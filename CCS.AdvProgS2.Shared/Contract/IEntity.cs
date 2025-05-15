namespace CCS.AdvProgS2.Shared.Contract;

public interface IEntity
{
	public EntityTypes EntityType { get; }
	public int XCoord { get; }
	public int YCoord { get; }
	public ConsoleColor Color { get; }
	public char Symbol { get; }
}