using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemScript : MonoBehaviour
{
    public string achievementName;
    
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject(-1))
        {
            AchievementManager.Instance.EarnAchievement(achievementName);
            
        }
    }
}
