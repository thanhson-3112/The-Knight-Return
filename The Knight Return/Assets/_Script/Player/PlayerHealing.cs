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

    private float currentSoul;
    public SoulManager soulManager;

    private float health;
    private float maxHealth;
    public PlayerLife playerLife;

    public CameraManager cameraManager;

    [Header("Sound")]
    public AudioClip focusHeallingSound;
    public AudioClip healingSound;


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

        // Nguoi choi an nut A hoi mau 
        if (Input.GetKeyDown(KeyCode.A) && isGround && currentSoul >=2 && health < maxHealth)
        {
            SoundFxManager.instance.PlaySoundFXClip(focusHeallingSound, transform, 1f);
            anim.SetBool("healing", true);
            canHeal = true;
            cameraManager.StartShrinkCamera(0.1f, 30f);
        }

        // Nguoi choi tha nut A khong hoi mau nua
        if (Input.GetKeyUp(KeyCode.A) || !isGround) 
        {
            SoundFxManager.instance.StopAudio(focusHeallingSound);
            anim.SetBool("healing", false);
            canHeal = false; 
            holdATimer = 0f; // reset thoi gian hoi mau
            anim.SetInteger("state", 0);
            cameraManager.StopShrinkCamera(); 
        }

        if (canHeal && health < maxHealth && currentSoul >= 2)
        {
            holdATimer += Time.deltaTime;
            if (holdATimer >= 2f && !isMoving) 
            {
                SoundFxManager.instance.PlaySoundFXClip(healingSound, transform, 1f);
                soulManager.MinusCurrentSoul();
                playerLife.PlayerHealing();
                holdATimer = 0f;
                cameraManager.StopShrinkCamera();
            }
        }

        // xet dk ko dc hoi mau
        if(health >= maxHealth || currentSoul < 2 || isMoving)
        {
            SoundFxManager.instance.StopAudio(focusHeallingSound);
            anim.SetBool("healing", false);
            canHeal = false;
            cameraManager.StopShrinkCamera();
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
    }
}
