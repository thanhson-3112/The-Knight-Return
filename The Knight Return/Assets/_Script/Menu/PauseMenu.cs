using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseMusicVolume;
    public GameObject resumeButton; 
    public static bool isPaused;
    public static bool isSetting;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseMusicVolume.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isSetting)
            {
                pauseMusicVolume.SetActive(false);
                isSetting = false;
            }
            else
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(resumeButton); 
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SettingGame()
    {
        pauseMusicVolume.SetActive(true);
        isSetting = true;
    }

    public void BackToMenu()
    {
        SaveManager.instance.SaveGame();
        SaveSystem.SaveToDisk();
        SceneManager.LoadScene(0);
    }
}
