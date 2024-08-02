using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public int maxHealth = 4;
    [SerializeField] public int health = 4;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private DarkScene darkScene;
    [SerializeField] private GameObject takeDamageEffect;

    [Header("CheckPoint")]
    [SerializeField] private TMP_Text checkPointText;
    private bool canActivateCheckpoint = false;
    public Vector2 respawnPoint = new Vector2(-283.04f, 114.50f);
    private Vector2 trapRespawnPoint;

    [Header("Sound")]
    [SerializeField] private AudioClip DamageSoundEffect;
    [SerializeField] private AudioClip DeathSoundEffect;
    [SerializeField] private AudioClip CheckpointSoundEffect;

    private bool invincible = false;
    [SerializeField] private CameraManager cameraManager;

    [Header("Player Gold")]
    [SerializeField] private GameObject DropGold;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] public int dieTime;

    [Header("Enemy controller")]
    [SerializeField] private GameObject enemyParent;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    private void Awake()
    {
        if (PlayerLife.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerLife.instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        StartCoroutine(PosPlayer());
    }

    public IEnumerator PosPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = respawnPoint;
    }

    public void Update()
    {
        enemyParent = GameObject.FindGameObjectWithTag("EnemyList");
        healthUI.SetMaxHealth(maxHealth);
        healthUI.SetHealth(health);

        if (canActivateCheckpoint && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            anim.SetTrigger("CheckPoint");
            SoundFxManager.instance.PlaySoundFXClip(CheckpointSoundEffect, transform, 1f);
            StartCoroutine(LockPlayerMove());
            respawnPoint = transform.position;
            health = maxHealth;
            Debug.Log("Checkpoint" + respawnPoint);

            // save game
            SaveManager.instance.SaveGame();
            SaveSystem.SaveToDisk();
        }

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"{health} / {maxHealth}";
    }

    IEnumerator LockPlayerMove()
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerMovement.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            cameraManager.ShakeCamera();
            healthUI.SetHealth(health);
            SoundFxManager.instance.PlaySoundFXClip(DamageSoundEffect, transform, 1f);

            Instantiate(takeDamageEffect, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(MakeInvincible(1f));
            }
        }
    }

    IEnumerator MakeInvincible(float time)
    {
        invincible = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        invincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            health--;
            SoundFxManager.instance.PlaySoundFXClip(DeathSoundEffect, transform, 1f);
            anim.SetTrigger("death");

            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(darkScene.ActivateDarkScene());
                Invoke("TrapRespawn", 2f);
            }
        }
    }

    private void TrapRespawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        transform.position = trapRespawnPoint;
        StartCoroutine(darkScene.DeactivateDarkScene());
    }

    private void Die()
    {
        SoundFxManager.instance.PlaySoundFXClip(DeathSoundEffect, transform, 1f);

        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("death");

        Instantiate(DropGold, transform.position, Quaternion.identity);
        StartCoroutine(ClearGold());

        if (playerGold.goldTotal == 0 && dieTime == 0)
        {
            dieTime += 0;
        }
        else
        {
            dieTime += 1;
        }

        playerAttack.enabled = false;
        StartCoroutine(darkScene.ActivateDarkScene());

        Invoke("Respawn", 2f);
    }

    IEnumerator ClearGold()
    {
        yield return new WaitForSeconds(2f);
        playerGold.ClearGold();
    }

    public void ResetDieTime()
    {
        dieTime = 0;
    }

    private void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        anim.SetTrigger("CheckPoint");
        transform.position = respawnPoint;
        health = maxHealth;
        playerAttack.enabled = true;

        StartCoroutine(darkScene.DeactivateDarkScene());
        ActivateAllEnemies(enemyParent);
    }

    private void ActivateAllEnemies(GameObject parent)
    {
        parent.SetActive(true);
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            if (child.childCount > 0)
            {
                ActivateAllEnemies(child.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            canActivateCheckpoint = true;
            checkPointText.gameObject.SetActive(true);
        }

        if (collision.CompareTag("TrapCheckPoint"))
        {
            trapRespawnPoint = collision.transform.position;
            Debug.Log("Checkpoint" + trapRespawnPoint);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            canActivateCheckpoint = false;
            checkPointText.gameObject.SetActive(false);
        }
    }

    public void PlayerHealing()
    {
        if (health < maxHealth)
        {
            health++;
        }
    }

    public void PlayerPray()
    {
        anim.SetTrigger("CheckPoint");
    }


    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.maxHealth = obj.maxHealth;
        this.health = obj.health;
        this.respawnPoint = obj.respawnPoint;
        Debug.Log("" + respawnPoint);
    }
}
