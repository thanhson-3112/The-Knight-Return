using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDoor : MonoBehaviour
{
    public Transform pointA; // ?i?m A
    public Transform pointB; // ?i?m B
    public float speed = 2.0f; // T?c ?? di chuy?n c?a cánh c?a

    private bool isOpen = false; // Tr?ng thái m? c?a

    // Update is called once per frame
    void Update()
    {
    }

    // Hàm m? c?a
    public void OpenDoor()
    {
        // Gán tr?ng thái m? c?a là true
        isOpen = true;

        // L?y v? trí hi?n t?i c?a cánh c?a
        Vector3 currentPosition = transform.position;

        // S? d?ng hàm Vector3.Lerp ?? di chuy?n t? v? trí hi?n t?i ??n ?i?m B v?i t?c ?? ???c ch? ??nh
        StartCoroutine(MoveDoor(currentPosition, pointB.position, speed));
    }

    // Coroutine di chuy?n c?a t? v? trí hi?n t?i ??n ?i?m B
    IEnumerator MoveDoor(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        float distance = Vector3.Distance(startPos, endPos); // Tính kho?ng cách gi?a v? trí hi?n t?i và v? trí ?ích

        // L?p cho ??n khi cánh c?a ??t ??n ?i?m ?ích
        while (distance > 0.01f)
        {
            // Di chuy?n cánh c?a t? v? trí hi?n t?i ??n v? trí ?ích v?i t?c ?? ???c ch? ??nh
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);

            // C?p nh?t l?i kho?ng cách gi?a v? trí hi?n t?i và v? trí ?ích
            distance = Vector3.Distance(transform.position, endPos);

            yield return null; // Ch? m?t frame m?i
        }
    }
}
