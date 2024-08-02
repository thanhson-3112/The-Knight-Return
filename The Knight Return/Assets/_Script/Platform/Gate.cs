using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;

    public void OpenDoor()
    {
        Vector3 currentPosition = transform.position;
        StartCoroutine(MoveDoor(currentPosition, pointA.position, speed));
    }

    IEnumerator MoveDoor(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        float distance = Vector3.Distance(startPos, endPos);

        while (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, endPos);
            yield return null;
        }
    }
}
