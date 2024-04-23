using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Check")]
    public Transform _isGround;
    public LayerMask Ground;
    private bool isGround;
    private bool isMoving;

    // Timer
    private float holdATimer;
    private bool canHeal;

    private int currentSoul;
    public SoulManager soulManager;

    private float health;
    private float maxHealth;
    public PlayerLife playerLife;

    [Header("Sound")]
    [SerializeField] private AudioSource focusHeallingSound;
    [SerializeField] private AudioSource healingSound;

    public  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();
        soulManager = playerObject.GetComponent<SoulManager>();

    }

    public void Update()
    {
        //Lay du lieu de xet dk heal
        maxHealth = playerLife.maxHealth;
        health = playerLife.health;

        currentSoul = soulManager.currentSoul;

        // Nguoi choi tha nut A hoi mau nua
        if (Input.GetKeyDown(KeyCode.A) && isGround && currentSoul > 0 && health < maxHealth)
        {
            focusHeallingSound.Play();
            anim.SetBool("healing", true);
            canHeal = true; 
        }

        // Nguoi choi tha nut A khong hoi mau nua
        if (Input.GetKeyUp(KeyCode.A) || !isGround) 
        {
            focusHeallingSound.Stop();
            anim.SetBool("healing", false);
            canHeal = false; 
            holdATimer = 0f; // reset thoi gian hoi mau
            anim.SetInteger("state", 0);
        }

        // neu nguoi choi di chuyen thi khong hoi mau
/*        if (isMoving)
        {
            focusHeallingSound.Stop();
            anim.SetBool("healing", false);
            canHeal = false;
        }*/

        if (canHeal && health < maxHealth && currentSoul >= 2)
        {
            holdATimer += Time.deltaTime;
            if (holdATimer >= 2.5f && !isMoving) 
            {
                healingSound.Play();
                soulManager.MinusCurrentSoul();
                playerLife.PlayerHealing();
                holdATimer = 0f; 
            }
        }

        if(health >= maxHealth || currentSoul < 2 || isMoving)
        {
            focusHeallingSound.Stop();
            anim.SetBool("healing", false);
            canHeal = false;
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
    }
}