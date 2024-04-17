using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour
{
    public GameObject musicVolume;

    public void Start()
    {
        musicVolume.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            musicVolume.SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingGame()
    {
        musicVolume.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
