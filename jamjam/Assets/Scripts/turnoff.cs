using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class turnoff : MonoBehaviour
{
    public void off()
    {
        gameObject.SetActive(false);
    }

    public void titleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void thisScene()
    {
        SceneManager.LoadScene(1);
    }
}
