using Unity.Netcode;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            TestServerRpc(0);
            TestDelayServerRpc(1000);
        }
    }

    [ClientRpc]
    void TestClientRpc(int value)
    {
        if (IsClient)
        {
            Debug.Log("Client Received the RPC #" + value);
            TestServerRpc(value + 1);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value)
    {
        Debug.Log("Server Received the RPC #" + value);
        TestClientRpc(value);
    }

    [ClientRpc]
    void TestDelayClientRpc(int value)
    {
        if (IsClient)
        {
            Debug.Log("!!!Client Received the Delay RPC #" + value);
            TestDelayServerRpc(value + 1);
        }
    }

    [ServerRpc]
    void TestDelayServerRpc(int value)
    {
        Debug.Log("!!!Server Received the Delay RPC #" + value);
        for(var i=0; i<10000; i++)
        {
            Debug.Log("-Delay for #" + value);
        }
        TestDelayClientRpc(value + 1);
    }
}