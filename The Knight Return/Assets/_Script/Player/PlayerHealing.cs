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
    private bool isHoldingA;
    private bool canHeal;


    private int currentSoul;
    public SoulManager soulManager;

    private float health;
    private float maxHealth;
    public PlayerLife playerLife;

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
        health = playerLife.health;
        maxHealth = playerLife.maxHealth;
        currentSoul = soulManager.currentSoul;

        // Nguoi choi tha nut A hoi mau nua
        if (Input.GetKeyDown(KeyCode.A) && isGround && currentSoul > 0 && health < maxHealth)
        {
            isHoldingA = true;
            anim.SetBool("healing", true);
            canHeal = true; 
        }

        // Nguoi choi tha nut A khong hoi mau nua
        if (Input.GetKeyUp(KeyCode.A) || !isGround) 
        {
            isHoldingA = false;
            anim.SetBool("healing", false);
            canHeal = false; 
            holdATimer = 0f; // reset thoi gian hoi mau
            anim.SetInteger("state", 0);
        }

        // neu nguoi choi di chuyen thi khong hoi mau
        if (isMoving)
        {
            anim.SetBool("healing", false);
            canHeal = false;
        }

        if (canHeal && health < maxHealth)
        {
            holdATimer += Time.deltaTime;
            if (holdATimer >= 2.5f && !isMoving) 
            {
                soulManager.MinusCurrentSoul();
                playerLife.PlayerHealing();
                holdATimer = 0f; 
            }
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
    }
}
