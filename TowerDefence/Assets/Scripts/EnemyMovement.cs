using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= enemy.speed / 100f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            OnEndPath();
        }
        else
        {
            wavepointIndex++;
            target = Waypoints.waypoints[wavepointIndex];
        }
    }

    void OnEndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
