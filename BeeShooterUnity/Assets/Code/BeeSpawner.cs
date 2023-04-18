using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public List<EnemyShip> enemyBeeBeefabs;

    public float numberOfBeesToSpawn;
    bool hasSpawnedBees;

    public float secondsBeetweenBeeSpawns;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>() && !hasSpawnedBees)
        {
            SpawnRoboBees();
        }
    }

    public void SpawnRoboBees()
    {
        print("SpawnBees");
        hasSpawnedBees = true;
        StartCoroutine(SpawnWaveOfBees());
    }

    private IEnumerator SpawnWaveOfBees()
    {
        for (int i = 0; i < numberOfBeesToSpawn; i++)
        {
            int rand = Random.Range(0, enemyBeeBeefabs.Count);
            Instantiate(enemyBeeBeefabs[rand], transform.position, transform.rotation, null);
            yield return new WaitForSeconds(secondsBeetweenBeeSpawns);
        }

        Destroy(gameObject);
    }
}
