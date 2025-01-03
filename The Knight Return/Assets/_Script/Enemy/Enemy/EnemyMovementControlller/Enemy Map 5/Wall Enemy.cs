using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3f;
    public GameObject[] wayPoints;

    int nextWayPoint = 1;
    private float distToPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWayPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, speed * Time.deltaTime);

        if (distToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    private void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.x += 180; 
        transform.eulerAngles = currRot;
        ChooseNextWaypoint();
    }

    private void ChooseNextWaypoint()
    {
        nextWayPoint++;
        if (nextWayPoint == wayPoints.Length)
        {
            nextWayPoint = 0;
        }
    }
}
