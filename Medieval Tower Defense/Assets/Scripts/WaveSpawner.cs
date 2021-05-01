using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
	public Transform spawnPoint;
	public Transform enemiesHolder;

    public float timeBetweenWaves = 5f;
	private float countdown = 2f;
	public float timeBetweenEnemies = 1f;
	public Text countdownBetweeWavesText;

	private int waveIndex = 0;

	void Update()
	{
		if(countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		countdownBetweeWavesText.text = Mathf.Round(countdown).ToString();
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;
		for (int i = 0; i < waveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(timeBetweenEnemies);
		}
	}

	void SpawnEnemy()
	{
		GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
		enemy.transform.parent = enemiesHolder.transform;
	}
}
