using UnityEngine;

public class test : MonoBehaviour
{
    public float speed = 5f; // T?c ?? di chuy?n

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Tính toán vector di chuy?n
        Vector2 moveVelocity = new Vector2(speed, rb.velocity.y);

        // Áp d?ng vector di chuy?n vào v?t
        rb.velocity = moveVelocity;
    }
}
