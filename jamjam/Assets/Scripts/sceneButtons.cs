using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneButtons : MonoBehaviour
{
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
