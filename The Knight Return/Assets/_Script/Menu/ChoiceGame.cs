using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class ChoiceGame : MonoBehaviour
{
    private string saveFileName = "Profile.bin"; 
    public GameObject newGamePanel;
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText; 

    public float currentProgress = 0f; 
    private float progressSpeed = 0.5f; 
    private bool isDone =  true;

    public void Start()
    {
        SaveSystem.Initialize(saveFileName); 
        newGamePanel.SetActive(false);
        loadingPanel.SetActive(false);
        currentProgress = 0f;
        loadingSlider.value = 0f;
        loadingText.text = "0%";
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            newGamePanel.SetActive(false);
        }

        if(currentProgress < 1f && !isDone)
        {
            currentProgress += Time.fixedDeltaTime * progressSpeed;

            currentProgress = Mathf.Clamp01(currentProgress);
            loadingSlider.value = currentProgress;
            loadingText.text = (currentProgress * 100f).ToString("F0") + "%";

            if (currentProgress >= 1f)
            {
                SceneManager.LoadScene(2);
                isDone = true;
            }
        }
    }

    public void Continue()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            ChoiceGameCheck.Instance.isNewGame = false;
            SaveSystem.Initialize(saveFileName);
        }
        else
        {
            ChoiceGameCheck.Instance.isNewGame = true;
        }

        loadingPanel.SetActive(true);
        ResetProgress();
    }

    public void NewGame()
    {
        newGamePanel.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void YesNewGame()
    {
        ChoiceGameCheck.Instance.isNewGame = true; 
        loadingPanel.SetActive(true);
        ResetProgress();
    }

    public void NoNewGame()
    {
        newGamePanel.SetActive(false);
    }

    private void ResetProgress()
    {
        currentProgress = 0f;
        loadingSlider.value = 0f;
        loadingText.text = "0%";
        isDone = false;
    }

    
}
