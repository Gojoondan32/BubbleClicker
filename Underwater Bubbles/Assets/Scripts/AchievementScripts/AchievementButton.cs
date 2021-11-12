using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public GameObject achievementList;
    public Sprite neutral, highlight;
    private Image sprite;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            Debug.Log("SpriteActive");
            sprite.sprite = highlight;
            achievementList.SetActive(true);
        }
        else
        {
            Debug.Log("SpriteInactive");
            sprite.sprite = neutral;
            achievementList.SetActive(false);
        }
    }
}
