using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float audienceGazeTimer = 0f;
    public float opponentGazeTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Audience")
            {
                Debug.Log("Hit audience.");
                audienceGazeTimer = audienceGazeTimer + Time.deltaTime;
            }
            else if (hit.collider.tag == "Opponent")
            {
                opponentGazeTimer += Time.deltaTime;
            }
        }
    }
}
