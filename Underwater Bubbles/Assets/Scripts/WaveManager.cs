using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public int numOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave[] wave;
    public Transform[] spawnpoints;

    private Wave currentWave;
    private int currentWaveNum;

    private float nextSpawnTime;

    public bool canSpawn = true;

    private Transform previousSpawn;

    [SerializeField]
    private BubbleManager bubbleManager;


    private void Start()
    {
        previousSpawn = null;
    }

    private void Update()
    {
        //Get the current wave
        currentWave = wave[currentWaveNum];
        SpawnWave();

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNum + 1 != wave.Length)
        {
            SpawnNextWave();
        }
    }

    void SpawnNextWave()
    {
        currentWaveNum++;
        canSpawn = true;
    }
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time) 
        {
            //Create a random game object from the typeOfEnemies array
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];

            //Choose a random spawn location
            Transform randomPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];

            //Make sure the new spawn point is not the same as the previous
            while (previousSpawn == randomPoint)
            {
                randomPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
            }
            
            previousSpawn = randomPoint;
            //Debug.Log(previousSpawn.transform.position);

            GameObject tempEnemy;
            tempEnemy = (GameObject)Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            //Decrase the total number of enemies after one has spawned 
            currentWave.numOfEnemies--;

            //Add list here
            bubbleManager.enemyList.Add(tempEnemy);

            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
        
    }

    
}
