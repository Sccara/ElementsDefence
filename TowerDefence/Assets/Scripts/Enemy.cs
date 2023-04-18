using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    
    private float health;

    public int reward = 50;

    public bool isSlowed = false;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    private void Update()
    {
        if(health <= 0)
            Die();
    }

    private void Die()
    {
        PlayerStats.Money += reward;

        GameObject effectIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 3f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
