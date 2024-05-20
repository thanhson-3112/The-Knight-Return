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

    //Sound
    [SerializeField] private AudioSource DamageSoundEffect;
    [SerializeField] private AudioSource DeathSoundEffect;
    [SerializeField] private AudioSource HeathSoundEffect;
    [SerializeField] private AudioSource CheckpointSoundEffect;

    public bool invincible = false;
    public CameraManager cameraManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
        respawnPoint = startPoint.transform.position;
    }

    public void Update()
    {
        healthUI.SetHealth(health);

        if (canActivateCheckpoint && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            anim.SetTrigger("CheckPoint");
            CheckpointSoundEffect.Play();
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
            DamageSoundEffect.Play();

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
            DeathSoundEffect.Play();
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
        if (DeathSoundEffect != null)
        {
            DeathSoundEffect.Play();
        }

        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        Invoke("Respawn", 1.7f);
    }

    private void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = respawnPoint;
        health = maxHealth;
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
