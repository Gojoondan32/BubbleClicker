using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement 
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string description;

    public string Desciption
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int points;

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    private int spriteIndex;

    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }

    private GameObject achievementRef;

    private List<Achievement> dependencies = new List<Achievement>();

    private string child;
    public string Child
    {
        get { return child; }
        set { child = value; }
    }

    //Variables to store the progression of an achievement
    private int currentProgression;
    private int maxProgression;

    

    public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef, int maxProgression)
    {
        //Sets the public paramaters to equal the private variables stored in the achievement script
        this.name = name;
        this.description = description;
        this.points = points;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        this.maxProgression = maxProgression;
        LoadAchievement();
    }

    public void AddDependency(Achievement dependency)
    {
        //Adds requirements
        dependencies.Add(dependency);
    }
    
    public bool EarnAchievement()
    {
        //Checks if an achivement has not already been unlocked as well as checking if the depencies have been cleared
        if (!unlocked && !dependencies.Exists(x => x.unlocked == false) && CheckProgress())
        {
            //Searches for the unlocked version of the achievement
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            SaveAchivement(true);

            if (child != null)
            {
                AchievementManager.Instance.EarnAchievement(child);
            }
            return true;
        }
        return false;
    }
    public void SaveAchivement(bool value)
    {
        //Saves the achievement and updates the amount of points 
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");
        

        Debug.Log("The current point value is: " + points);

        PlayerPrefs.SetInt("Points", tmpPoints += points);

        PlayerPrefs.SetInt("Progression" + name, currentProgression);

        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
    }
    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            Debug.Log("Achievement Saved");
            AchievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            currentProgression = PlayerPrefs.GetInt("Progression" + name);
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            
        }
    }

    public bool CheckProgress()
    {
        currentProgression++;
        //Updates the progress of an achievement 
        achievementRef.transform.GetChild(0).GetComponent<Text>().text = Name + " " + currentProgression + "/" + maxProgression;

        SaveAchivement(false);

        //Debug.Log(Name.ToString());

        if(maxProgression == 0 || currentProgression >= maxProgression)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
