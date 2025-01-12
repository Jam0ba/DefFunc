using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
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
        int randNum = Random.Range(0, spawnPoints.Length);
        GameObject enemyOBJ = Instantiate(enemy, spawnPoints[randNum].position, Quaternion.identity);
    }

    private IEnumerator SpawnTime()
    {
        spawned = false;
        yield return new WaitForSeconds(5.0f);
        spawned = true;
        SpawnEnemy();
    }
}
