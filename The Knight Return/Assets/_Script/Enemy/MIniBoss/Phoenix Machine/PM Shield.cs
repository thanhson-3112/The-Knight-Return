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
                    // N?u ???ng k�nh ?ang t?ng v� ?� ??t t?i ?a
                    if (isIncreasing && shieldRadius >= maxShieldRadius)
                    {
                        isIncreasing = false; // ?�nh d?u chuy?n sang gi?m ???ng k�nh
                    }
                    // N?u ???ng k�nh ?ang gi?m v� ?� ??t t?i thi?u
                    else if (!isIncreasing && shieldRadius <= minShieldRadius)
                    {
                        isIncreasing = true; // ?�nh d?u chuy?n sang t?ng ???ng k�nh
                        shieldMove = false;
                    }

                    // T?ng ho?c gi?m ???ng k�nh t�y theo tr?ng th�i
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
            // T�nh to�n v? tr� m?i c?a khi�n
            float angle = Time.time * -rotateSpeed;
            float x = PM.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * shieldRadius;
            float y = PM.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * shieldRadius;
            Vector3 newPosition = new Vector3(x, y, transform.position.z);

            // Di chuy?n khi�n ??n v? tr� m?i
            transform.position = newPosition;

            // Quay khi�n quanh tr?c z
            Quaternion newRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = newRotation;
        }
        else
        {
            Debug.LogWarning("PM ch?a ???c g�n cho khi�n.");
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