using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public TextMeshProUGUI waveTimerText;

    public GameManager gameManager;

    public float wavesTimer = 5.5f;

    private Enemy enemyScript;

    private float countdown = 2.5f;

    private int waveIndex = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = wavesTimer;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveTimerText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);

            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        if (waveIndex == waves.Length)
        {
            StartCoroutine(gameManager.OnWinLevel());
            enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
