using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    // Timer
    private float holdATimer;
    private bool isHoldingA;

    public PlayerLife playerLife;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();
    }

    void Update()
    {
        // Check if holding A button
        if (Input.GetKeyDown(KeyCode.A))
        {
            isHoldingA = true;
            anim.SetBool("healing", true);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isHoldingA = false;
            anim.SetBool("healing", false);
        }

        if (isHoldingA)
        {
            holdATimer += Time.deltaTime;
            if (holdATimer >= 3f)
            {
                playerLife.PlayerHealing();
            }
        }
        else
        {
            holdATimer = 0f;
        }

        // Stop player movement if holding A
        if (isHoldingA)
        {
            // Stop player movement
            rb.velocity = Vector2.zero;
            anim.SetInteger("state", 0);
        }
    }
}
