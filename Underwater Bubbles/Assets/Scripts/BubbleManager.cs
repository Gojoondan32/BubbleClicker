using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubble;
    public GameObject coin;
    
    Vector3 startPos;
    Vector3 endPos;

    public GameObject objStart;
    public GameObject objEnd;

    Vector3 previousPos;
    Vector3 randomPos;

    private float currentTime = 0f;
    private float spawnTime = 0.5f;
    [SerializeField]
    private float coinTime = 10f;

    [SerializeField]
    private Stats health;

    
    private float bubbleTime = 1f;


    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> bubbleList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //Get the starting and ending points from two game objects
        startPos = new Vector3(objStart.transform.position.x, objStart.transform.position.y, objStart.transform.position.z);
        endPos = new Vector3(objEnd.transform.position.x, objEnd.transform.position.y, objEnd.transform.position.z);

        health.Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > spawnTime)
        {
            //Get a random x coordinate between these points
            float x = Random.Range(startPos.x, endPos.x);
            //Set X variable to a vector 
            randomPos = new Vector3(x, -6, 0);

            while (randomPos == previousPos)
            {
                //Get a random x coordinate between these points
                x = Random.Range(startPos.x, endPos.x);
                //Set X variable to a vector 
                randomPos = new Vector3(x, -6, 0);
            }

            previousPos = randomPos;
           

            //Spawn the bubble at the random position
            GameObject tempBubble;
            tempBubble = (GameObject)Instantiate(bubble, randomPos, Quaternion.identity);
            
            bubbleList.Add(tempBubble);
            currentTime = 0;
            
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if (coinTime <= 0)
        {
            Debug.Log("Coin spawn");
            GameObject tempCoin = (GameObject)Instantiate(coin, randomPos, Quaternion.identity);
            coinTime = 10f;
        }
        else
        {
            coinTime -= Time.deltaTime;
        }
        CheckBubbleStatus();
        CheckEnemyStatus();
        

        //Decrease the health by 1 every second
        if(bubbleTime <= 0f)
        {
            health.CurrentVal -= 1;
            bubbleTime = 1f;
        }
        else
        {
            bubbleTime -= Time.deltaTime;
        }


        if(health.CurrentVal <= 0)
        {
            AchievementManager.Instance.EarnAchievement("Death");
            FindObjectOfType<GameManager>().GameOver();
        }
        

    }
    
    private void CheckBubbleStatus()
    {
        //Loop through each bubble in the scene and increase health when it is destroyed
        for (int i = bubbleList.Count - 1; i >= 0; i--)
        {
            if (bubbleList[i] == null && Bubble.isDestroyed)
            {
                health.CurrentVal += 1;
                bubbleList.Remove(bubbleList[i]);
            }
            else if (bubbleList[i] == null)
            {
                bubbleList.Remove(bubbleList[i]);
            }
        }
    }


    
    private void CheckEnemyStatus()
    {
        //Loop through the enemy list and decrease health when they are destroyed;
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            
            if (enemyList[i] == null && FishMovement.enemyDestroyed)
            {
                health.CurrentVal -= 10;
                health.MaxVal -= 10;
                enemyList.Remove(enemyList[i]);
            }
            else if (enemyList[i] == null)
            {
                enemyList.Remove(enemyList[i]);
            }
        }
    }
}
