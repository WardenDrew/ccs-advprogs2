using System.Runtime.InteropServices;
using CCS.AdvProgS2.Shared.Contract;
using CCS.AdvProgS2.Shared.Contract.Messages;

namespace CCS.AdvProgS2.Shared.Implementations.Demo;

/// <summary>
/// A Demo Game Client Implementation that doesn't network
/// </summary>
public class DemoGameClient : IGameClient
{
	private const int MAX_RECENT_MESSAGES = 5;

	// These should be tracked by the real client anyway
	// Here we set these variables in each method, in the real client
	// they should be set by the message coming from the server.

	private bool isConnected = false;
	private string? currentPlayerName = null;
	private Guid currentPlayerGuid = Guid.Empty;
	private readonly List<IChatMessage> recentMessages = [];

	// The level
	private Level level;

	// Manually track just one player's position for now
	// The Server will need to track ALL player positions or positions of other entities
	private int playerXCoord = 2;
	private int playerYCoord = 2;

	/// <summary>
	/// Constructor
	/// </summary>
	public DemoGameClient()
	{
		// Generate a demo level
		level = new()
		{
			Id = 0,
			Name = "Demo Level",
			Width = 10,
			Height = 10,
		};

		for (int x = 0; x < level.Width; x++)
		{
			for (int y = 0; y < level.Height; y++)
			{
				// If this is the first or last column or the first or last row, add a wall
				if (x == 0 ||
					x == level.Width - 1 ||
					y == 0 ||
					y == level.Height - 1)
				{
					level.Entities.Add(new WallEntity()
					{
						XCoord = x,
						YCoord = y,
						Color = ConsoleColor.Blue
					});

					// continue next loop iteration
					continue;
				}

				// Otherwise fill the rest with floor
				level.Entities.Add(new FloorEntity()
				{
					XCoord = x,
					YCoord = y,
					Color = ConsoleColor.White
				});
			}
		}
	}

	

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
			PlayerName = this.currentPlayerName ?? "UNKNOWN", // Null-Coalesce null currentPlayerName to the string "UNKNOWN"
			TimeStamp = DateTime.Now,
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
		// Store starting coords in case new coords aren't valid
		int newX = playerXCoord;
		int newY = playerYCoord;

		switch (inputType)
		{
			case InputTypes.UP:
				newY--;
				break;
			case InputTypes.DOWN:
				newY++;
				break;
			case InputTypes.LEFT:
				newX--;
				break;
			case InputTypes.RIGHT:
				newX++;
				break;
		}

		// Check if this is out of bounds
		if (newX < 0 || newX >= level.Width || newY < 0 || newY >= level.Height)
		{
			return false;
		}

		// Check if the new position is a wall
		bool hasWallAtCoord = level.Entities
			.Where(e =>
				e.XCoord == newX &&
				e.YCoord == newY &&
				e.EntityType == EntityTypes.Wall)
			.Any();
		if (hasWallAtCoord)
		{
			return false;
		}

		playerXCoord = newX;
		playerYCoord = newY;

		return true;
	}
	
	/// <inheritdoc/>
	public List<IChatMessage> GetRecentMessages()
	{
		return recentMessages;
	}

	/// <inheritdoc/>
	public ILevel GetCurrentLevel()
	{
		// get the level from the server. here we createa a copy of the level from our stored state
		// That way when we add the player into it here manually, we don't corrupt the master level state

		ILevel responseLevel = new Level()
		{
			Id = level.Id,
			Name = level.Name,
			Width = level.Width,
			Height = level.Height
		};
		// Fill the responseLevel object's entities list with the master entities list for this level
		responseLevel.Entities.AddRange(level.Entities.AsReadOnly());

		// Add the player
		responseLevel.Entities.Add(new PlayerEntity()
		{
			XCoord = playerXCoord,
			YCoord = playerYCoord,
			Color = ConsoleColor.Red
		});

		return responseLevel;
    }
}