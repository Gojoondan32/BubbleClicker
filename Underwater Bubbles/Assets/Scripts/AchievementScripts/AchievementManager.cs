using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    public GameObject achievementPrefab;

    public Sprite[] sprites;

    private AchievementButton activeButton;
    public ScrollRect scrollRect;

    public GameObject achievementMenu;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockedSprite;

    public Text textPoints;

    private static AchievementManager instance;

    private int fadeTime = 2;

    

    public static AchievementManager Instance
    {
        get { 
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance; 
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Delete previous achievements
        PlayerPrefs.DeleteAll();

        //Create achievements 
        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();
        
        //CreateAchievement("General", "All keys", "Press all keys to unlock this achievement", 10, 0, 0, new string[] {"Press W", "Press A", "Press S", "Press D" });
        CreateAchievement("General", "Bubble", "Press a bubble to unlock this achievement", 5, 0, 0);
        //CreateAchievement("General", "Bubble Supremacy", "Press 100 bubbles to unlock this achievement", 20, 2, 100);
        CreateAchievement("General", "Fish Suprise", "Hit a fish for the first time", 5, 1, 0);
        //CreateAchievement("General", "Fish Feast", "Hit 8 fish to unlock this achievement", 20, 1, 8);
        //CreateAchievement("General", "Bubble Heaven", "Press 1000 bubbles to unlock this achievement", 50, 0, 1000);
        CreateAchievement("General", "Shark Attack", "Hit a shark for the first time", 10, 4, 0);
        //CreateAchievement("General", "Bubble Master", "Press 10,000 bubbles to unlock this achievement", 100, 2, 10000);
        CreateAchievement("General", "Pufferfish", "Hit a pufferfish for the first time", 5, 5, 0);
        CreateAchievement("General", "Coin Master", "Earn 1000 score", 25, 6, 0);
        


        CreateAchievement("Other", "Death", "Die for the first time", 10, 3, 0);
        



        //Set all objects inside the achievement list to false at the start
        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }
        

        activeButton.Click();

        achievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Unlock achievements
        if (Input.GetKeyDown(KeyCode.I))
        {
            achievementMenu.SetActive(!achievementMenu.activeSelf);
        }
        
    }
    public void EarnAchievement(string title)
    {
        //Create the achievement on screen and update our dictionary to mark the achievment as complete
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            StartCoroutine(FadAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        //Destroy the achievement after it has appeared on the screen
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }
    public void CreateAchievement(string parent, string title, string description, int points, int spriteIndex, int progress, string[] dependencies = null)
    {
        //Creates an achievement prefab
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);
        //Creates new parameters for the achievement
        Achievement newAchievement = new Achievement(title, description, points, spriteIndex, achievement, progress);
        //Adds the newley created achievement to the dictionary
        achievements.Add(title, newAchievement);
        SetAchievementInfo(parent, achievement, title, progress);

        //Checks if the created achievement has any dependencys needed for it to be unlocked
        if(dependencies != null)
        {
            foreach( string achivementTitle in dependencies)
            {
                Achievement dependency = achievements[achivementTitle];
                dependency.Child = title;
                newAchievement.AddDependency(dependency);

                //Dependency = press space <-- Child = press w
                //NewAchivement = press w --> press space
            }
        }

        


    }
    public void SetAchievementInfo(string parent, GameObject achievement, string title, int progression = 0)
    {
        //Sets the parent for the created achievement (sets which category its under)
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        //Adjusts its scale back to its original size
        achievement.transform.localScale = new Vector3(1, 1, 1);

        //Checks the progress of the achievement
        string progress = progression > 0 ? " " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString() : string.Empty;

        //Sets the information inside of the achievement as a child of its parent object
        achievement.transform.GetChild(0).GetComponent<Text>().text = title + progress;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Desciption;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }
    public void ChangeCategory(GameObject button)
    {
        //Changes which category is currently being shown in the achievement menu
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();

        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();
        achievementButton.Click();
        activeButton.Click();

        activeButton = achievementButton;
    }

    private IEnumerator FadAchievement(GameObject achievement)
    {
        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

        
        //Adjusts the alpha value of the instansiated achievement after 2 seconds have passed
        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievement);
    }
}
