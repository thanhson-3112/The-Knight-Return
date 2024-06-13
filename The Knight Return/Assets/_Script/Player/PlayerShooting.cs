using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    private float currentSoul;
    public SoulManager soulManager;

    [SerializeField] private bool lockFireBall = true;

    [Header("Sound")]
    public AudioClip FireBallSound;

    void Start()
    {
    }

    void Update()
    {
        currentSoul = soulManager.currentSoul;
        Shoot();
    }

    private void Shoot()
    {
        if ((Input.GetKeyDown(KeyCode.V) && fireTimer <= 0f) && currentSoul >= 2 && !lockFireBall)
        {
            SoundFxManager.instance.PlaySoundFXClip(FireBallSound, transform, 1f);
            fireTimer = fireRate;

            Vector3 shootDirection = transform.right; 
            if (transform.localScale.x < 0) 
            {
                shootDirection = -transform.right;
            }

            GameObject fireBall = Instantiate(firePrefab, firingPoint.position, Quaternion.identity);
            fireBall.transform.right = shootDirection;

            soulManager.MinusCurrentSoul();
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void UnlockFireBall()
    {
        lockFireBall = false;
    }
}
