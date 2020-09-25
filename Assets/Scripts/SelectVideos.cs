using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SelectVideos : MonoBehaviour
{
    public VideoPlayer player;
    public List<VideoClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        SelectVideo();
        player.Play();
    }

    public void SelectVideo()
    {
        int rand = Random.Range(0, clips.Count);
        player.clip = clips[rand];
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isLooping)
        {
            SceneManager.LoadScene(0);
        }
    }
}
