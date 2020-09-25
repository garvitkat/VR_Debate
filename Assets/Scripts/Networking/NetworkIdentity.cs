using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Project.Utility.Attributes;

namespace Project.Networking
{
    public class NetworkIdentity : MonoBehaviour
    {
        [Header("Useful data")]
        [GrayOut] [SerializeField] private string id;
        [GrayOut] [SerializeField] private bool isControlling;

        private SocketIOComponent socket;

        // Start is called before the first frame update
        public void Awake()
        {
            isControlling = false;
        }

        // Update is called once per frame
        public void SetControllerID(string ID)
        {
            id = ID;
            //Check incoming id vs the server id
            isControlling = (NetworkClient.ClientID == ID) ? true : false;
        }

        public void SetSocketReference(SocketIOComponent Socket)
        {
            socket = Socket;
        }

        public string GetID()
        {
            return id;
        }

        public bool IsControlling()
        {
            return isControlling;
        }

        public SocketIOComponent GetSocket()
        {
            return socket;
        }
    }
}
