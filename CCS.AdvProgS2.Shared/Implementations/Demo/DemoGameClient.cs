using CCS.AdvProgS2.Shared.Contract;
using CCS.AdvProgS2.Shared.Contract.Messages;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

/// <summary>
/// A Demo Game Client Implementation that doesn't network
/// </summary>
public class DemoGameClient : IGameClient
{
	private const int MAX_RECENT_MESSAGES = 5;
	
	private bool isConnected = false;
	private string? currentPlayerName = null;
	private Guid currentPlayerGuid = Guid.Empty;

	private readonly List<IChatMessage> recentMessages = [];
	
	/// <inheritdoc/>
	public bool JoinServer(string serverAddress, string playerName, string? serverPassword = null)
	{
		// Nothing to do here since we don't actually network
		
		// Safety check player name was something not empty
		if (string.IsNullOrWhiteSpace(playerName))
		{
			throw new InvalidOperationException("You specified a blank player name!");
		}
		
		currentPlayerName = playerName;
		currentPlayerGuid = Guid.NewGuid();
		isConnected = true;

		return isConnected;
	}
	
	/// <inheritdoc/>
	public void LeaveServer()
	{
		currentPlayerName = null;
		currentPlayerGuid = Guid.Empty;
		isConnected = false;
	}

	/// <inheritdoc/>
	public List<IPlayer> GetConnectedPlayers()
	{
		// Safety check that we are connected to a server
		if (!isConnected)
		{
			throw new InvalidOperationException("You are not connected to a server!");
		}
		
		// Safety check player name was something not empty
		if (string.IsNullOrWhiteSpace(this.currentPlayerName))
		{
			throw new InvalidOperationException("You specified a blank player name!");
		}
		
		// Since we aren't networked there will only be one player so we create this list here for the demo
		List<IPlayer> players = new();
		
		Player player = new()
		{
			Id = this.currentPlayerGuid,
			Name = this.currentPlayerName,
		};

		players.Add(player);
		
		return players;
	}

	/// <inheritdoc/>
	public bool SendChatMessage(string chatMessage)
	{
		// Safety check that we are connected to a server
		if (!isConnected)
		{
			throw new InvalidOperationException("You are not connected to a server!");
		}
		
		// Create a new chat message object
		ChatMessage message = new()
		{
			PlayerId = this.currentPlayerGuid,
			Timestamp = DateTime.Now,
			Message = chatMessage
		};

		// Add this message to our recent messages
		recentMessages.Add(message);

		// If we have more than max recent message history, remove the first one
		if (recentMessages.Count > MAX_RECENT_MESSAGES)
		{
			recentMessages.RemoveAt(0);
		}

		return true;
	}
	
	/// <summary>
	/// NOT IMPLEMENTED IN THE DEMO CLIENT
	/// </summary>
	/// <param name="inputType"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public bool SendInputMessage(InputTypes inputType)
	{
		// No gamestate for the demo client
		throw new NotImplementedException();
	}
	
	/// <summary>
	/// NOT IMPLEMENTED IN THE DEMO CLIENT
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public IGameState GetGameState()
	{
		// No gamestate for the demo client
		throw new NotImplementedException();
	}
	
	/// <inheritdoc/>
	public List<IChatMessage> GetRecentMessages()
	{
		return recentMessages;
	}
}