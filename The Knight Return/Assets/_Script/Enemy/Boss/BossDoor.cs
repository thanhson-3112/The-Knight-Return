using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDoor : MonoBehaviour
{
    public Transform pointA; // ?i?m A
    public Transform pointB; // ?i?m B
    public float speed = 2.0f; // T?c ?? di chuy?n c?a c�nh c?a

    private bool isOpen = false; // Tr?ng th�i m? c?a

    // Update is called once per frame
    void Update()
    {
    }

    // H�m m? c?a
    public void OpenDoor()
    {
        // G�n tr?ng th�i m? c?a l� true
        isOpen = true;

        // L?y v? tr� hi?n t?i c?a c�nh c?a
        Vector3 currentPosition = transform.position;

        // S? d?ng h�m Vector3.Lerp ?? di chuy?n t? v? tr� hi?n t?i ??n ?i?m B v?i t?c ?? ???c ch? ??nh
        StartCoroutine(MoveDoor(currentPosition, pointB.position, speed));
    }

    // Coroutine di chuy?n c?a t? v? tr� hi?n t?i ??n ?i?m B
    IEnumerator MoveDoor(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        float distance = Vector3.Distance(startPos, endPos); // T�nh kho?ng c�ch gi?a v? tr� hi?n t?i v� v? tr� ?�ch

        // L?p cho ??n khi c�nh c?a ??t ??n ?i?m ?�ch
        while (distance > 0.01f)
        {
            // Di chuy?n c�nh c?a t? v? tr� hi?n t?i ??n v? tr� ?�ch v?i t?c ?? ???c ch? ??nh
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);

            // C?p nh?t l?i kho?ng c�ch gi?a v? tr� hi?n t?i v� v? tr� ?�ch
            distance = Vector3.Distance(transform.position, endPos);

            yield return null; // Ch? m?t frame m?i
        }
    }
}
