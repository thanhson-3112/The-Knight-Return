using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";

    private void Awake()
    {
        if (SaveManager.instance != null) Debug.LogError("Ch? cho phép m?t SaveManager");
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    protected virtual string GetSaveName()
    {
        return SaveManager.SAVE_1;
    }

    protected virtual string GetSaveName1()
    {
        return SaveManager.SAVE_2;
    }

    public virtual void LoadSaveGame()
    {
        string jsonString = SaveSystem.GetString(this.GetSaveName());
        string jsonString1 = SaveSystem.GetString(this.GetSaveName1());
        PlayerGold.instance.FromJson(jsonString);
        SoulManager.instance.FromJson(jsonString1);
        Debug.Log("loadSaveGame " + jsonString);
        Debug.Log("loadSaveGame " + jsonString1);
    }

    public virtual void SaveGame()
    {
        Debug.Log("SaveGame");
        string jsonString = JsonUtility.ToJson(PlayerGold.instance);
        string jsonString1 = JsonUtility.ToJson(SoulManager.instance);
        SaveSystem.SetString(this.GetSaveName(), jsonString);
        SaveSystem.SetString(this.GetSaveName1(), jsonString1);
    }
}
