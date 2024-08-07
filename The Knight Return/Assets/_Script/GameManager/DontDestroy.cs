using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public Camera mainCamera;

    [Header("Skill Guide")]
    public GameObject healingGuide;
    public GameObject inventorylGuide;
    public GameObject fireballGuide;
    public GameObject dashGuide;
    public GameObject doubleJumpGuide;
    public GameObject slideWallGuide;

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

        miniMapShop1 = Resources.Load<GameObject>("MiniMap/Shop MiniMap Manager 1");
        miniMapShop2 = Resources.Load<GameObject>("MiniMap/Shop MiniMap Manager 2");
        miniMapShop3 = Resources.Load<GameObject>("MiniMap/Shop MiniMap Manager 3");
        miniMapShop4 = Resources.Load<GameObject>("MiniMap/Shop MiniMap Manager 4");

        healingGuide = Resources.Load<GameObject>("Skill/Healing Guide");
        inventorylGuide = Resources.Load<GameObject>("Skill/Inventory Guide"); 
        fireballGuide = Resources.Load<GameObject>("Skill/FireBall Guide");
        dashGuide = Resources.Load<GameObject>("Skill/Dash Guide");
        doubleJumpGuide = Resources.Load<GameObject>("Skill/DoubleJump Guide");
        slideWallGuide = Resources.Load<GameObject>("Skill/SlideWall Guide");
    }
}
