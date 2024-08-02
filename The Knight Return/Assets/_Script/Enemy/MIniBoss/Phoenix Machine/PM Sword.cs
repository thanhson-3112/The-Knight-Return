using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMSword : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private int damage = 1;

    public PlayerLife playerLife;
    public PlayerMovement player;
    private Rigidbody2D rb;

    private bool hasStartedMoving = false;

    [Header("Sound")]
    public AudioClip swordSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        if (!hasStartedMoving)
        {
            StartCoroutine(StartMovingAfterDelay());
            hasStartedMoving = true;
        }
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity = transform.right * speed;
        SoundFxManager.instance.PlaySoundFXClip(swordSound, transform, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.KnockFromRight = false;
            }
            
            playerLife.TakeDamage(damage);
        }
    }
}
