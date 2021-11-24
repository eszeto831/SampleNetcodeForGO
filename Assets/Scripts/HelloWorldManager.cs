using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        void OnGUI()
        {
            Debug.Log("edmond :: on GUI");
#if !UNITY_SERVER
            Debug.Log("edmond :: please dont come in here for server build");
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
#endif
        }

        public virtual void Awake()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            var cur_time = (System.DateTime.UtcNow - epochStart).TotalMilliseconds;
            Debug.Log("edmond :: test 4");
            Debug.Log("edmond :: unity awake: " + cur_time);
            Screen.fullScreen = false;
        }

        public virtual void Start()
        {
            // headless mode? then start the server
            // can't do this in Awake because Awake is for initialization.
            // some transports might not be ready until Start.
            Application.targetFrameRate = 60;
#if UNITY_SERVER
            NetworkManager.Singleton.StartServer();
#endif
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();
            }
        }
    }
}