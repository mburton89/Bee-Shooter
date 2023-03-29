using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public float numberOfBeesToSpawn;
    bool hasSpawnedBees;

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

        numberOfBeesToSpawn--;

        for (int i = 0; i < numberOfBeesToSpawn; i++)
        {

        }
    }

    public void CheckNumberOfBeesLeft()
    {
        print(numberOfBeesToSpawn);

        if (numberOfBeesToSpawn > 0)
        {
            SpawnRoboBees();
        }
    }



}
