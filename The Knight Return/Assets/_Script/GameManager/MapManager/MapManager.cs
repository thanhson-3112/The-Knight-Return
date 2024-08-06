using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [Header("Scene Names")]
    public string map1SceneName = "Map 1";
    public string map2SceneName = "Map 2";
    public string map3SceneName = "Map 3";
    public string map4SceneName = "Map 4";
    public string map5SceneName = "Map 5";

    [Header("Map Active States")]
    public bool map1Active;
    public bool map2Active;
    public bool map3Active;
    public bool map4Active;
    public bool map5Active;

    private void Awake()
    {
        if (MapManager.instance != null)
        {
            Debug.LogError("Ch? ???c phép có 1 MapManager");
        }
        MapManager.instance = this;
    }

    void Start()
    {
        UpdateMapActiveStates();
    }

    void Update()
    {
        // C?p nh?t tr?ng thái map
        map1Active = IsSceneLoaded(map1SceneName);
        map2Active = IsSceneLoaded(map2SceneName);
        map3Active = IsSceneLoaded(map3SceneName);
        map4Active = IsSceneLoaded(map4SceneName);
        map5Active = IsSceneLoaded(map5SceneName);
    }

    void UpdateMapActiveStates()
    {
        // S? d?ng bi?n bool ?? xét tr?ng thái active
        LoadSceneIfNeeded(map1SceneName, map1Active);
        LoadSceneIfNeeded(map2SceneName, map2Active);
        LoadSceneIfNeeded(map3SceneName, map3Active);
        LoadSceneIfNeeded(map4SceneName, map4Active);
        LoadSceneIfNeeded(map5SceneName, map5Active);
    }

    public void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.map1Active = obj.map1Active;
        this.map2Active = obj.map2Active;
        this.map3Active = obj.map3Active;
        this.map4Active = obj.map4Active;
        this.map5Active = obj.map5Active;

        UpdateMapActiveStates();
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void LoadSceneIfNeeded(string sceneName, bool shouldBeActive)
    {
        if (shouldBeActive && !IsSceneLoaded(sceneName))
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        else if (!shouldBeActive && IsSceneLoaded(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
