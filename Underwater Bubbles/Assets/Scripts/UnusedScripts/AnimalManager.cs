using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    
    public Transform[] enemyList;
    public List<AnimalStats> animalStats = new List<AnimalStats>();
    // Start is called before the first frame update
    void Start()
    {
        animalStats.Add(new AnimalStats("Fish", 2));
        animalStats.Add(new AnimalStats("Shark", 5));
        animalStats.Add(new AnimalStats("Seahorse", 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickRandomAnimal(Transform[] enemyList)
    {
        int index = Random.Range(0, enemyList.Length);
        Transform newEnemy = enemyList[index];
        
        
    }
}
