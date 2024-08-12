using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public Camera mainCamera;

    public GameObject skillText;
    public PlayerShooting playerFireBall;
    public PlayerMovement playerSkill;
    public PlayerLife playerPray;
    public PlayerMovement playerMovement;

    [Header("MiniMap")]
    public GameObject miniMapShop1;
    public GameObject miniMapShop2;
    public GameObject miniMapShop3;
    public GameObject miniMapShop4;

    public GameObject bossName;

    private Dictionary<string, GameObject> allObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CacheAllObjects();
        InitializeReferences();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CacheAllObjects();
        InitializeReferences();
    }

    private void CacheAllObjects()
    {
        // Clear previous cache
        allObjects.Clear();

        GameObject[] allGameObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.activeInHierarchy)
            {
                if (!string.IsNullOrEmpty(obj.tag))
                {
                    allObjects[obj.tag] = obj;
                }
            }
        }
    }

    private void InitializeReferences()
    {
        mainCamera = Camera.main;

        // Access cached objects
        if (allObjects.TryGetValue("UpArrow", out GameObject skillTextObj))
        {
            skillText = skillTextObj;
        }
        else
        {
            Debug.LogWarning("Skill object with tag 'UpArrow' not found.");
        }

        if (allObjects.TryGetValue("Player", out GameObject playerObject))
        {
            playerFireBall = playerObject.GetComponent<PlayerShooting>();
            playerSkill = playerObject.GetComponent<PlayerMovement>();
            playerPray = playerObject.GetComponent<PlayerLife>();
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogWarning("Player object not found.");
        }

        if (allObjects.TryGetValue("MiniMapManager1", out GameObject miniMap1))
        {
            miniMapShop1 = miniMap1;
        }

        if (allObjects.TryGetValue("MiniMapManager2", out GameObject miniMap2))
        {
            miniMapShop2 = miniMap2;
        }

        if (allObjects.TryGetValue("MiniMapManager3", out GameObject miniMap3))
        {
            miniMapShop3 = miniMap3;
        }

        if (allObjects.TryGetValue("MiniMapManager4", out GameObject miniMap4))
        {
            miniMapShop4 = miniMap4;
        }

        if (allObjects.TryGetValue("BossName", out GameObject bossNameObj))
        {
            bossName = bossNameObj;
        }
        else
        {
            Debug.LogWarning("Boss name object not found.");
        }
    }
}
