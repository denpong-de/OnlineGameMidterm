using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public short playerPrefabIndex;
    public string[] selStrings = new string[] { "P1", "P2", "P3", "P4" };
    public int selGridInt = 0;

    private void OnGUI()
    {
        if (!isNetworkActive)
        {
            selGridInt = GUI.SelectionGrid(new Rect(Screen.width - 200, 10, 200, 50),
                selGridInt, selStrings, 2);

            playerPrefabIndex = (short)(selGridInt + 1);
        }
    }

    public void SwitchPlayer(SetupLocalPlayer player, int cid)
    {
        Vector3 pPosition = player.gameObject.transform.position;
        Quaternion pRotation = player.gameObject.transform.rotation;
        player.gameObject.SetActive(false);
        GameObject newPlayer = Instantiate(spawnPrefabs[cid], pPosition, pRotation);
        playerPrefab = spawnPrefabs[cid];
        NetworkServer.ReplacePlayerForConnection(player.connectionToClient, newPlayer, 0);
        Destroy(player.gameObject);
    }

    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler(MsgTypes.PlayerPrefabSelect, OnResponsePrefab);
        base.OnStartServer();
    }

    private void OnResponsePrefab(NetworkMessage netMsg)
    {
        MsgTypes.PlayerPrefabMsg msg = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>();
        playerPrefab = spawnPrefabs[msg.prefabIndex];
        base.OnServerAddPlayer(netMsg.conn, msg.controllerID);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        client.RegisterHandler(MsgTypes.PlayerPrefabSelect, OnRequestPrefab);
        base.OnClientConnect(conn);
    }

    private void OnRequestPrefab(NetworkMessage netMsg)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        msg.controllerID = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>().controllerID;
        msg.prefabIndex = playerPrefabIndex;
        client.Send(MsgTypes.PlayerPrefabSelect, msg);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, 
        short playerControllerId)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        msg.controllerID = playerControllerId;
        NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefabSelect, msg);

    }
}

public class MsgTypes
{
    public const short PlayerPrefabSelect = MsgType.Highest + 1;
    public class PlayerPrefabMsg : MessageBase
    {
        public short controllerID;
        public short prefabIndex;
    }
}
