using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AchievementManager : MonoBehaviour
{
    private static AchievementManager _instance;
    public static AchievementManager Instance { get { return _instance; } }


    // Singleton Pattern
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private AudioSource audio;
    private Text achievemntText;
    private Image achievementImage;

    // Achievement Variables
    public int ItemsCount = 0;

    private event Action UpdateAchievementForItems;


    //public void UpdateAchievementForItems()
    //{
    //    ItemsCount++;
    //    CheckForAchievements();
    //}

    //private void CheckForAchievements()
    //{
    //    CollectedFiveItems();
    //}

    //private bool CollectedFiveItems()
    //{

    //}

    //public void DisplayAchievement(string message)
    //{

    //}

    public void SuperZombieAchievement()
    {

    }



}
