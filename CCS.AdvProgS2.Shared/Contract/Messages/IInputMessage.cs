namespace CCS.AdvProgS2.Shared.Contract.Messages;

public interface IInputMessage : IMessage
{
	public InputTypes InputType { get; }
}