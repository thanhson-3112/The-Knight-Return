using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnload;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(LoadScenes());
            StartCoroutine(UnloadScenes());
        }
    }

    private IEnumerator LoadScenes()
    {
        foreach (var sceneField in _sceneToLoad)
        {
            if (!IsSceneLoaded(sceneField.SceneName))
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneField.SceneName, LoadSceneMode.Additive);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
                Debug.Log("Loaded: " + sceneField.SceneName);
                UpdateMapManager(sceneField.SceneName, true);
            }
        }
    }

    private IEnumerator UnloadScenes()
    {
        foreach (var sceneField in _sceneToUnload)
        {
            if (IsSceneLoaded(sceneField.SceneName))
            {
                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneField.SceneName);
                while (!asyncUnload.isDone)
                {
                    yield return null;
                }
                Debug.Log("Unloaded: " + sceneField.SceneName);
                UpdateMapManager(sceneField.SceneName, false);
            }
        }
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

    private void UpdateMapManager(string sceneName, bool isActive)
    {
        if (sceneName == MapManager.instance.map1SceneName) MapManager.instance.map1Active = isActive;
        if (sceneName == MapManager.instance.map2SceneName) MapManager.instance.map2Active = isActive;
        if (sceneName == MapManager.instance.map3SceneName) MapManager.instance.map3Active = isActive;
        if (sceneName == MapManager.instance.map4SceneName) MapManager.instance.map4Active = isActive;
        if (sceneName == MapManager.instance.map5SceneName) MapManager.instance.map5Active = isActive;
    }
}
