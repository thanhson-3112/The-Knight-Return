using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMController : MonoBehaviour
{
    private Animator anim;
    public GameObject RaFireball;
    private bool isAttacking = false;
    private bool nearAttacking = false;
    public GameObject player;

    public bool canUpdate = false; 


    void Start()
    {
        anim = GetComponent<Animator>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitBeforeStart());
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(3f); 
        canUpdate = true; 
    }

    private void Update()
    {
        if (canUpdate)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= 10f)
            {
                if (!nearAttacking)
                {
                    Debug.Log("gan 10f");
                    StartCoroutine(MoveAttack());
                    nearAttacking = true;
                }
            }
            else
            {
                if (!isAttacking)
                {
                    Debug.Log("xa 10f");
                    StartCoroutine(ShootAttack());
                    isAttacking = true;
                }
            }
        }
    }

    IEnumerator MoveAttack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(3f);
        nearAttacking = false;
    }


    IEnumerator ShootAttack()
    {
        FireBallShoot();
        anim.SetBool("isMoving", true);
        yield return new WaitForSeconds(4f);
        isAttacking = false; 
    }

    private void FireBallShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            float offsetAngle = (i - 1) * 60f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            // Instantiate bullet
            GameObject spawnedEnemy = Instantiate(RaFireball, transform.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
        }
    }
}
