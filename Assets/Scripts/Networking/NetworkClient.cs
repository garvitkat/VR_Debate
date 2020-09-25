using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

namespace Project.Networking
{
    public class NetworkClient : SocketIOComponent
    {
        [Header("Network Client")]
        [SerializeField] private Transform networkContainer;
        [SerializeField] private Dictionary<string, NetworkIdentity> serverObjects;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject audiencePrefab;

        [SerializeField] private TestSceneScript testScene;
        // new added code
        [SerializeField] private List<Transform> spawns;
        //[SerializeField] private Transform audience;
        [SerializeField] private int playerCount = 0;

        public static string ClientID { get; private set; }
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            Initialize();
            SetupEvents();
        }

        public void Initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }

        public void Disconnect()
        {
            On("disconnected", (E) =>
            {
                // Handling disconnect
                string id = E.data["id"].ToString().Replace('"', ' ');

                GameObject go = serverObjects[id].gameObject;
                go.GetComponent<NetworkIdentity>().GetSocket().Emit("disconnect");
                Destroy(go); // remove from scene
                serverObjects.Remove(id); // remove from memory
            });
        }

        public void SetupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("Connection made to the server");
            });

            On("register", (E) =>
            {
                ClientID = E.data["id"].ToString().Replace('"', ' ');
                playerCount = playerCount + 1;
                Debug.LogFormat("Our client id  = ({0})", ClientID);
            });

            On("spawn", (E) =>
            {
                // Handling spawn and passing data
                string id = E.data["id"].ToString().Replace('"', ' ');

                Transform tempTransform = spawns[int.Parse(E.data["playerCount"].ToString())-1];

                //tempTransform.position = new Vector3(x, y, z);
                //tempTransform.rotation = Quaternion.Euler(new Vector3(0, 180));
                GameObject go = Instantiate(int.Parse(E.data["playerCount"].ToString()) > 2 ? audiencePrefab : playerPrefab, tempTransform);
                go.name = String.Format("Player ({0})", id);
                NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
                ni.SetControllerID(id);
                ni.SetSocketReference(this);
                serverObjects.Add(id, ni);
            });
            Disconnect();

            On("disconnected", (E) =>
            {
                // Handling disconnect
                string id = E.data["id"].ToString().Replace('"', ' ');

                GameObject go = serverObjects[id].gameObject;
                Destroy(go); // remove from scene
                serverObjects.Remove(id); // remove from memory
            });

            On("updatePosition", (E) =>
            {
                string id = E.data["id"].ToString().Replace('"', ' ');

                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, y, z);
            });
        }
    }

    [Serializable]
    public class Player
    {
        public string id;
        public bool isSpeaker;
        public Position position;
    }

    [Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }

}