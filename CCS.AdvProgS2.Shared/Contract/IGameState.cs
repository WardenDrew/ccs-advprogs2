namespace CCS.AdvProgS2.Shared.Contract;

public interface IGameState
{
	public List<ILevel> Levels { get; }
	public List<IPlayer> Players { get; }
}