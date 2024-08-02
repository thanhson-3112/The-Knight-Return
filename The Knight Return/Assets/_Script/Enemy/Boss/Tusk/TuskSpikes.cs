using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskSpikes : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10f;
    protected int damage = 1;

    public PlayerLife playerLife;
    public PlayerMovement player;
    public GameObject target; 

    private Vector2 initialPosition;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        initialPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MoveUp());

        Destroy(gameObject, 6);
    }

    private IEnumerator MoveUp()
    {
        // Ch?n m?t h??ng di chuy?n ng?u nhiên
        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;

        // Bay theo h??ng ng?u nhiên ???c ch?n
        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Ki?m tra n?u ??i t??ng ?ã di chuy?n ?? xa
            if (Vector3.Distance(transform.position, initialPosition) >= 2f)
            {
                break; // Thoát vòng l?p n?u ??i t??ng ?ã di chuy?n ?? xa
            }

            yield return null;
        }

        isFalling = true; // B?t ??u r?i xu?ng
        yield return MoveDown();
    }

    private IEnumerator MoveDown()
    {
        Vector3 targetPosition = target.transform.position;

        // Tính toán h??ng và l?c ?? ??i t??ng r?i theo ???ng parabol
        Vector2 direction = (targetPosition - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPosition);
        float gravity = Physics2D.gravity.magnitude;
        float angle = 45f * Mathf.Deg2Rad; // Góc b?n 45 ??

        // Tính toán v?n t?c ban ??u
        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle));

        // Tính toán các thành ph?n v?n t?c theo tr?c x và y
        float vx = velocity * Mathf.Cos(angle);
        float vy = velocity * Mathf.Sin(angle);

        rb.velocity = new Vector2(vx * direction.x, vy);

        // Ch? cho ??n khi ??i t??ng ch?m ??t ho?c ??t ??n m?c tiêu
        while (rb.velocity.y <= 0 && !isFalling)
        {
            yield return null;
        }

        isFalling = false;
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
