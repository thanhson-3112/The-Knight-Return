using System.Collections;
using System.Collections.Generic;
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

    //CHeckPoint
    private Vector2 respawnPoint;
    public GameObject startPoint;

    //Sound
    [SerializeField] private AudioSource DamageSoundEffect;
    [SerializeField] private AudioSource DeathSoundEffect;
    [SerializeField] private AudioSource HeathSoundEffect;
    [SerializeField] private AudioSource CheckpointSoundEffect;

  

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
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthUI.SetHealth(health);
        DamageSoundEffect.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
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
        if (collision.gameObject.CompareTag("Blood"))
        {
            if (health != maxHealth)
            {
                HeathSoundEffect.Play();
                Destroy(collision.gameObject);
                health++;
            }
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            HeathSoundEffect.Play();
            Destroy(collision.gameObject);
            maxHealth++;
            health = maxHealth;
        }

        if (collision.CompareTag("CheckPoint"))
        {
            CheckpointSoundEffect.Play();
            respawnPoint = collision.transform.position;
            Debug.Log("Checkpoint" + respawnPoint);
        }
    }

    public void PlayerHealing()
    {
        if (health < maxHealth) 
        {
            health++;
        }
    }
                        

    private void RestartLevel()
    {
        transform.position = respawnPoint;
        health = maxHealth;
    }
}
