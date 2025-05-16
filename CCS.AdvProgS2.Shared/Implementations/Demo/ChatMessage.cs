using CCS.AdvProgS2.Shared.Contract.Messages;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class ChatMessage : IChatMessage
{
	public required string PlayerName { get; set; }
	public DateTime TimeStamp { get; set; }
	public required string Message { get; set; }
}