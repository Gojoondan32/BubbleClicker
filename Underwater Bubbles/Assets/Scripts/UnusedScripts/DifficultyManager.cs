using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting};

    [System.Serializable]
    public class Wave
    {
        public string name;
        //Needs to be array
        public Transform enemy;
        public int count;
        public float spawnRate;

    }

    //Create an array of the wave class
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnpoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.counting;

    private void Start()
    {
        if (spawnpoints.Length == 0)
        {
            Debug.Log("No spawn points");
        }

        waveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        if(state == SpawnState.waiting)
        {
            //Check if current wave is alive
            if (!EnemyIsAlive())
            {
                //Begin new wave
                BeginNewWave();
                return;
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.spawning)
            {
                //Start spawning waves
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void BeginNewWave()
    {
        Debug.Log("Wave complete");

        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Completed all waves! Looping...");
        }
        else
        {
            nextWave++;
        }

        
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.spawning;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy" + _enemy.name);

        //Choose random spawn
        Transform _sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        
    }
}
