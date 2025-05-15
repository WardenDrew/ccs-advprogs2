using CCS.AdvProgS2.Shared.Contract.Messages;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

public class ChatMessage : IChatMessage
{
	public Guid PlayerId { get; set; }
	public DateTime Timestamp { get; set; }
	public required string Message { get; set; }
}