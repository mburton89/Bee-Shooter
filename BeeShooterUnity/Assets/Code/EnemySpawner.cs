using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyShip> enemyShipPrefabs;
    public Transform spawnPoint;
    public Transform spawnPivot;

    [HideInInspector] public int currentWave = 1;
    [HideInInspector] public int startingNumberOfShips;
    // Start is called before the first frame update

    private void Awake()
    {
        startingNumberOfShips = FindObjectsOfType<EnemyShip>().Length;

    }
    public void SpawnEnemies()
    {
        int enemyShipsToSpawn = startingNumberOfShips + currentWave - 1;

        for (int i = 0; i < enemyShipsToSpawn; i++)
        {
            int rand = Random.Range(0, enemyShipPrefabs.Count);
            float zRotation = Random.Range(0, 360);

            spawnPivot.eulerAngles = new Vector3(0, 0, zRotation);

            Instantiate(enemyShipPrefabs[rand], spawnPoint.position, transform.rotation);
        }
    }
    public void CountEnemyShips()
    {
        int numberofEnemyShips = FindObjectsOfType<EnemyShip>().Length;

        print(numberofEnemyShips);

        if (numberofEnemyShips == 1)
        {
            currentWave+= 1;
            HUD.instance.DisplayWave(currentWave);
            SpawnEnemies();
        }
    }
}
