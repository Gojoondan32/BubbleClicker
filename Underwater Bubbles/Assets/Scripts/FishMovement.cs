using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private float destroyTime = 15f;
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private bool isActive = false;

    public static bool enemyDestroyed;

    public GameObject enemyParticles;

    

    // Start is called before the first frame update
    void Start()
    {
        enemyDestroyed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //Add destroy time but check if the fish is off the screen first
        if(destroyTime <= 0 && !isActive)
        {
            enemyDestroyed = false;
            Destroy(gameObject);
        }

        
        //Turn the fish when it exceeds a certain x position
        if(gameObject.transform.position.x <= -14)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (gameObject.transform.position.x >= 10)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    private void OnMouseDown()
    {
        //Earn fish achievement
        AchievementManager.Instance.EarnAchievement("Fish Suprise");
        //AchievementManager.Instance.EarnAchievement("Fish Feast");

        //Start the camera shake funciton
        FindObjectOfType<Shake>().ShakeCamera();

        enemyDestroyed = true;

        GameManager.highScore -= 25;

        //Create the particles
        Destroy(Instantiate(enemyParticles, transform.position, Quaternion.identity) as GameObject, 1);

        //Destroy the game object
        Destroy(gameObject);
    }

    //Used to detect if the fish is visible by any cameras 
    private void OnBecameInvisible()
    {
        isActive = false;
    }
    private void OnBecameVisible()
    {
        isActive = true;
    }
}
