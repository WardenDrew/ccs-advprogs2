namespace CCS.AdvProgS2.Shared.Contract;

public interface IMessage
{
	public Guid PlayerId { get; }
	public DateTime Timestamp { get; }
}