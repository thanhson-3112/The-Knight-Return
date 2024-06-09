using UnityEngine;

public class PMShield : MonoBehaviour
{
    public GameObject PM;
    public float shieldRadius = 10f; 
    public float maxShieldRadius = 20f; 
    public float minShieldRadius = 10f; 
    private float timer = 0f;
    private bool isIncreasing = true; 

    public float rotateSpeed = 50f;

    public bool shieldMove = false;

    void Update()
    {
        if (PM != null)
        {

            if(shieldMove == true)
            {
                timer += Time.deltaTime;
                if (timer >= 0.3f)
                {
                    // N?u ???ng kính ?ang t?ng và ?ã ??t t?i ?a
                    if (isIncreasing && shieldRadius >= maxShieldRadius)
                    {
                        isIncreasing = false; // ?ánh d?u chuy?n sang gi?m ???ng kính
                    }
                    // N?u ???ng kính ?ang gi?m và ?ã ??t t?i thi?u
                    else if (!isIncreasing && shieldRadius <= minShieldRadius)
                    {
                        isIncreasing = true; // ?ánh d?u chuy?n sang t?ng ???ng kính
                        shieldMove = false;
                    }

                    // T?ng ho?c gi?m ???ng kính tùy theo tr?ng thái
                    if (isIncreasing)
                    {
                        shieldRadius += 1f;
                        rotateSpeed = 300f;
                    }
                    else
                    {
                        shieldRadius -= 1f;
                        rotateSpeed = 300f;
                    }

                    timer = 0f; // ??t l?i bi?n ??m th?i gian
                }
            }

            else
            {
                rotateSpeed = 50f;
            }
            // Tính toán v? trí m?i c?a khiên
            float angle = Time.time * -rotateSpeed;
            float x = PM.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * shieldRadius;
            float y = PM.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * shieldRadius;
            Vector3 newPosition = new Vector3(x, y, transform.position.z);

            // Di chuy?n khiên ??n v? trí m?i
            transform.position = newPosition;

            // Quay khiên quanh tr?c z
            Quaternion newRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = newRotation;
        }
        else
        {
            Debug.LogWarning("PM ch?a ???c gán cho khiên.");
        }
    }

    public void ShieldMove()
    {
        shieldMove = true;
    }

    public void StopShieldMove()
    {
        shieldMove = false;
    }
}