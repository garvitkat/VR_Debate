using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Networking;

namespace Project.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Class References")]
        [SerializeField] private NetworkIdentity networkIdentity;

        void FixedUpdate()
        {
            if (networkIdentity.IsControlling())
            {
            }
        }
    }
}
