namespace CCS.AdvProgS2.Shared.Contract.Entities;

public interface IDoorEntity : IEntity
{
	public bool IsLocked { get; }
	public char LockedSymbol { get; }
	public ConsoleColor LockedColor { get; }
}