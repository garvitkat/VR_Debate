using System.Collections;
using System.Collections.Generic;
using Project.Utility.Attributes;
using UnityEngine;

namespace Project.Networking
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class NetworkTransform : MonoBehaviour
    {
        [GrayOut] [SerializeField] private Vector3 oldPosition;
        private NetworkIdentity networkIdentity;
        private Player player;

        private float stillCounter = 0f;
        // Start is called before the first frame update
        void Start()
        {
            networkIdentity = GetComponent<NetworkIdentity>();
            oldPosition = transform.position;
            player = new Player();
            player.position = new Position();
            player.position.x = 0;
            //player.position.y = 2;
            player.position.z = 0;

            if (!networkIdentity.IsControlling())
            {
                enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (networkIdentity.IsControlling())
            {
                if (oldPosition != transform.position)
                {
                    oldPosition = transform.position;
                    stillCounter = 0;
                    SendData();
                }
                else
                {
                    stillCounter += Time.deltaTime;

                    if (stillCounter >= 1)
                    {
                        stillCounter = 0;
                        SendData();
                    }
                }
            }
        }

        private void SendData()
        {
            // Update player information on server
            player.position.x = (Mathf.Round(transform.position.x * 1000f)) / 1000f;
            //player.position.y = (Mathf.Round(transform.position.y * 1000f)) / 1000f;
            player.position.z = (Mathf.Round(transform.position.z * 1000f)) / 1000f;

            networkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));
        }
    }
}