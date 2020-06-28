using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneButtons : MonoBehaviour
{
    public GameObject blackScreen;

    public void restartScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<Animator>().SetTrigger("Game");
    }

    public void backToTitle()
    {
        //SceneManager.LoadScene(0);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<Animator>().SetTrigger("Title");
    }
}
