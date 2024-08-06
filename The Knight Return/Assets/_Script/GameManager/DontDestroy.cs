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
            SceneManager.sceneLoaded += OnSceneLoaded; // ??ng k� s? ki?n khi scene ???c t?i
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

    // Ph??ng th?c n�y s? ???c g?i m?i khi m?t scene m?i ???c t?i
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
    }

    // Ph??ng th?c ?? g�n c�c tham chi?u
    private void InitializeReferences()
    {
        mainCamera = Camera.main; // G�n mainCamera t?i ?�y
        skillText = GameObject.FindGameObjectWithTag("UpArrow")?.GetComponent<TMP_Text>();
        playerFireBall = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerShooting>();
        playerSkill = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerMovement>();
        playerPray = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerLife>();
        playerMovement = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerMovement>();

        // G�n c�c object minimap n?u c?n thi?t
        miniMapShop1 = GameObject.Find("Shop MIniMap Manager 1");
        miniMapShop2 = GameObject.Find("Shop MIniMap Manager 2");
        miniMapShop3 = GameObject.Find("Shop MIniMap Manager 3");
        miniMapShop4 = GameObject.Find("Shop MIniMap Manager 4");
    }
}
