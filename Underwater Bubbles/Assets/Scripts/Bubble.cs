using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour
{
    public float speed = 2f;
    public GameObject deathEffect;
    public static bool isDestroyed;
    public string achievementName;

    public string additionalAchievementName;

    private bool firstAchievement;
    private bool secondAchievement;
    private bool thirdAchievement;
    private void Start()
    {
        isDestroyed = false;
        firstAchievement = false;
        secondAchievement = false;
        thirdAchievement = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        //Destroy bubbles when they reach above the screen
        if(gameObject.transform.position.y >= 6)
        {
            
            Destroy(gameObject);
        }
    }
    public void OnMouseDown()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject(-1))
        {
            //Achievements that can be earned
            AchievementManager.Instance.EarnAchievement(achievementName);
            //AchievementManager.Instance.EarnAchievement(additionalAchievementName);
            //AchievementManager.Instance.EarnAchievement("Bubble Heaven");
            //AchievementManager.Instance.EarnAchievement("Bubble Master");
        }

        isDestroyed = true;
        Destroy(gameObject);
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject, 1);
        GameManager.highScore += 1;
    }
    
}
