using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundEnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
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

        if(distToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    private void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWaypoint();
    }

    private void ChooseNextWaypoint()
    {
        nextWayPoint++;
        if(nextWayPoint == wayPoints.Length)
        {
            nextWayPoint = 0;
        }
    }
}
