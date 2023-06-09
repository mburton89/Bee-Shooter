using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public int numberOfStarsToSpawn;
    public float maxX;
    public float maxY;
    public float minZ;
    public float maxZ;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfStarsToSpawn; i++)
        {
            float randX = Random.Range(-maxX, maxX);
            float randY = Random.Range(-maxY, maxY);
            float randZ = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(randX, randY, randZ);
            Instantiate(starPrefab, spawnPosition, transform.rotation);
        }
    }

    
}
