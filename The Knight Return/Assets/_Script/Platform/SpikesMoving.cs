using System.Collections;
using UnityEngine;

public class SpikesMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 40f;
    [SerializeField] private float waitTime = 1f; // Th?i gian d?ng l?i t?i m?i waypoint

    private bool isWaiting = false;

    private void Update()
    {
        if (!isWaiting)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                StartCoroutine(WaitAtWaypoint());
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            }
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
        isWaiting = false;
    }
}
