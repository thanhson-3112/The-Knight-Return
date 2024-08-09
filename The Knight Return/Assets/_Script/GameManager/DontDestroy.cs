using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public Camera mainCamera;

    public TMP_Text skillText;
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
        InitializeReferences();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        mainCamera = Camera.main; // Gán mainCamera t?i ?ây
        skillText = GameObject.FindGameObjectWithTag("UpArrow").GetComponent<TMP_Text>();
        playerFireBall = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerPray = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        miniMapShop1 = GameObject.FindGameObjectWithTag("MiniMapManager1");
        miniMapShop2 = GameObject.FindGameObjectWithTag("MiniMapManager2");
        miniMapShop3 = GameObject.FindGameObjectWithTag("MiniMapManager3");
        miniMapShop4 = GameObject.FindGameObjectWithTag("MiniMapManager4");

        bossName = GameObject.FindGameObjectWithTag("BossName");
    }

}
