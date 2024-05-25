using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public int maxHealth = 5;
    [SerializeField] public int health;

    public HealthUI healthUI;
    public GameObject takeDamageEffect;

    [Header("CheckPoint")]
    [SerializeField] private TMP_Text checkPointText;
    private bool canActivateCheckpoint = false;
    private Vector2 respawnPoint;
    private Vector2 trapRespawnPoint;
    public GameObject startPoint;

    [Header("Sound")]
    public AudioClip DamageSoundEffect;
    public AudioClip DeathSoundEffect;
    public AudioClip CheckpointSoundEffect;

    private bool invincible = false;
    public CameraManager cameraManager;

    [Header("Player Gold")]
    public GameObject DropGold;
    public PlayerGold playerGold;
    public int dieTime;

    public GameObject enemyParent;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
        respawnPoint = startPoint.transform.position;
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
    }

    public void Update()
    {
        healthUI.SetHealth(health);

        if (canActivateCheckpoint && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            anim.SetTrigger("CheckPoint");
            SoundFxManager.instance.PlaySoundFXClip(CheckpointSoundEffect, transform, 1f);
            respawnPoint = transform.position;
            health = maxHealth;
            Debug.Log("Checkpoint" + respawnPoint);
        }
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
                StartCoroutine(MakeInvincible(1.5f));
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

            if (health <= 0)
            {
                Die();
            }

            Invoke("TrapRespawn", 1f);
        }
    }

    private void TrapRespawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = trapRespawnPoint;
    }

    private void Die()
    {
        SoundFxManager.instance.PlaySoundFXClip(DeathSoundEffect, transform, 1f);

        rb.bodyType = RigidbodyType2D.Static;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        anim.SetTrigger("death");

        // Spawn gold when the player dies
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

        Invoke("Respawn", 1.7f);
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
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        transform.position = respawnPoint;
        health = maxHealth;

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
}
