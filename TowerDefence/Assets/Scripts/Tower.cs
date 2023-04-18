using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Use Bullets")]

    public bool useBullets = false;
    private float fireCountDown = 0f;
    public float fireRate = 1f;

    [Header("Use Poison")]

    public bool usePoison = false;

    public int damageOverTime = 30;
    public int poisonDuration = 5;

    [Header("Slowing Down")]

    public bool slowingDown = false;

    public float slowAmount = 0.5f;

    [Header("Attributes")]

    public float range = 15f;
    public float turnSpeed = 10f;

    [Header("Unity Setup Fields")]

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform partToRotate;
    public string enemyTag = "Enemy";

    private Transform target;
    private Enemy targetEnemy;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < range && slowingDown)
                SlowingDown(enemy);

            if (distanceToEnemy > range && slowingDown)
                enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().startSpeed;

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (partToRotate != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (fireCountDown <= 0f && useBullets)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.FindTarget(target);
    }

    void SlowingDown(GameObject enemyToSlow)
    {
        enemyToSlow.GetComponent<Enemy>().Slow(slowAmount);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
