using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOption : MonoBehaviour
{
    public int index;
    public Material inactiveMaterial;
    public Material activeMaterial;
    public Renderer myRenderer;
    public TextMesh text;

    private bool isGazedAt = false;

    public void CheckClick()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(index);
        }

    }

    public void SetActive()
    {
        myRenderer.material = activeMaterial;
        text.color = Color.black;
    }

    public void SetInactive()
    {
        myRenderer.material = inactiveMaterial;
        text.color = Color.white;
    }
}
