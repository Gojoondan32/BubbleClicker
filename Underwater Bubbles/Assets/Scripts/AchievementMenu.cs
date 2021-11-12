using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMenu : MonoBehaviour
{

    public GameObject achievementMenu;
    private void OnMouseDown()
    {
        //Show the achievement menu
        achievementMenu.SetActive(!achievementMenu.activeSelf);
    }
}
