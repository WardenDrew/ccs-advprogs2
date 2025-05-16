namespace CCS.AdvProgS2.Shared.Contract.Messages;

public interface IChatMessage
{
	public string PlayerName { get; }
	public DateTime TimeStamp { get; }

	public string Message { get; }
	
}