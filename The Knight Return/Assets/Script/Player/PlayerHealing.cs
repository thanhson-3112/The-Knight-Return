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

    //Soul
    [SerializeField] 
    private float maxSoul = 5;
    private float soul;

    public SoulUI soulUI;
    public PlayerLife playerLife;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        soul = maxSoul;
        soulUI.SetMaxSoul(maxSoul);
    }

    void Update()
    {
        // Nguoi choi tha nut A khong hoi mau nua
        if (Input.GetKeyDown(KeyCode.A) && isGround && soul > 0)
        {
            isHoldingA = true;
            anim.SetBool("healing", true);
            canHeal = true; // B?t c? cho phép h?i máu
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

        if (canHeal)
        {
            holdATimer += Time.deltaTime;
            if (holdATimer >= 3f && !isMoving) 
            {
                soul--;
                soulUI.SetSoul(soul);
                playerLife.PlayerHealing();
                holdATimer = 0f; 
            }
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
    }
}
