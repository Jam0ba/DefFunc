using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private Transform[] spawnPoints;

    bool spawned;

    private void Start()
    {
        StartCoroutine(SpawnTime());
    }
    private void Update()
    {
        if (spawned)
        {
            StartCoroutine(SpawnTime());
        }
    }
    private void SpawnEnemy()
    {
        int randNumPos = Random.Range(0, spawnPoints.Length);
        int randNumEnemy = Random.Range(0, enemy.Length);

        GameObject enemyOBJ = Instantiate(enemy[randNumEnemy], spawnPoints[randNumPos].position, Quaternion.identity);
    }

    private IEnumerator SpawnTime()
    {
        spawned = false;
        yield return new WaitForSeconds(1.8f);
        spawned = true;
        SpawnEnemy();
    }
}
