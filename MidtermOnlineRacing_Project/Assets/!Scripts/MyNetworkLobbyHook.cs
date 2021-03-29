using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class MyNetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
        GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupLocalPlayer actualPlayer = gamePlayer.GetComponent<SetupLocalPlayer>();
        actualPlayer.pName = lobby.playerName;
        actualPlayer.pColor = lobby.playerColor;
    }
}
