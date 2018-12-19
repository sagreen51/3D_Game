using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LoadingMenu()
    {
        StartCoroutine(loadscreen(.3f, 1));

    }

    public void PlayGame()
    {
        StartCoroutine(loadscreen(.3f, 2));


    }

    public void Instructions()
    {
        StartCoroutine(loadscreen(.3f, 3));

    }

    public void Credits()
    {
        StartCoroutine(loadscreen(.3f, 4));

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator loadscreen(float time, int screenIndex)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(screenIndex);
    }

}
