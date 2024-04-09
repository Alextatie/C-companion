using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Stage(int s)
    {
        SceneManager.LoadScene(s);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
