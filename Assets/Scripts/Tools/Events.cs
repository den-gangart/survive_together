using Unity.Services.Lobbies.Models;

public interface GameEvent {}

public class NameChangeEvent: GameEvent
{
    public string name;
}

public class JoinLobbyEvent : GameEvent
{
    public Lobby lobby;
}

public class CreateLobbyEvent : GameEvent
{
    public Lobby lobby;
}

public class ConnectEvent: GameEvent { }
public class LeftLobbyEvent: GameEvent {}
public class StartGameSessionEvent : GameEvent {}
public class SignInEvent: GameEvent {}
public class SignOutEvent: GameEvent {}