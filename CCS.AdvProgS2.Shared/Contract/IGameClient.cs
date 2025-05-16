using CCS.AdvProgS2.Shared.Contract.Messages;

namespace CCS.AdvProgS2.Shared.Contract;

public interface IGameClient
{
	/// <summary>
	/// Join a Server
	/// </summary>
	/// <param name="serverAddress">The address of the server to join</param>
	/// <param name="playerName">The Name of the player joining the server</param>
	/// <param name="serverPassword">The Optional password to join the server</param>
	/// <returns>True or false if the server join was successful</returns>
	public bool JoinServer(string serverAddress, string playerName, string? serverPassword = null);
	
	/// <summary>
	/// Leave a Server
	/// </summary>
	public void LeaveServer();

	/// <summary>
	/// Send a chat message to the currently connected server
	/// </summary>
	/// <param name="chatMessage">The message to send</param>
	/// <returns>True or false if sending the message was successful</returns>
	public bool SendChatMessage(string chatMessage);
	
	/// <summary>
	/// Send an Input message to the currently connected server
	/// </summary>
	/// <param name="inputType">The input to send</param>
	/// <returns>True or false if sending the input was successful</returns>
	public bool SendInputMessage(InputTypes inputType);

	/// <summary>
	/// Get the latest level state from the server
	/// </summary>
	/// <returns>an ILevel object from the server</returns>
	public ILevel GetCurrentLevel();

	/// <summary>
	/// Get recent chat messages from the server
	/// </summary>
	/// <returns></returns>
	public List<IChatMessage> GetRecentMessages();
}