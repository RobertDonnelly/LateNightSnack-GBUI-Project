using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

 
  public void Play()
    {
        SceneManager.LoadSceneAsync("Level");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

}
