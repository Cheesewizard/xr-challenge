using System;
using System.Collections;
using TMPro;
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


    private void OnEnable()
    {
        StatsManager.Instance.onEnemysKilled += EnemiesKilled;
        StatsManager.Instance.onItemsCollected += ItemsFound;
        StatsManager.Instance.onDamageGiven += DamageGiven;
        GameManager.Instance.OnLevelComplete += GameComplete;
    }


    private void OnDisable()
    {
        StatsManager.Instance.onEnemysKilled -= EnemiesKilled;
        StatsManager.Instance.onItemsCollected -= ItemsFound;
        StatsManager.Instance.onDamageGiven -= DamageGiven;
        GameManager.Instance.OnLevelComplete -= GameComplete;
    }


    [SerializeField]
    private string scoreHeader = "Achievement";
    public TextMeshProUGUI achievementUi;

    [SerializeField]
    private int displayTime = 3;





    private IEnumerable DisplayAcheivement(string message)
    {
        var formatted = string.Format(scoreHeader, message, Environment.NewLine).ToString();
        achievementUi.text = formatted;

        yield return new WaitForSeconds(displayTime);

        achievementUi.text = string.Empty;
    }


    private bool gameComplete;
    private void GameComplete()
    {
        if (!gameComplete)
        {
            DisplayAcheivement("Congrats, you have completed the game");
            gameComplete = true;
        }
    }


    private bool itemsFoundAchievement;
    private void ItemsFound(int itemsFound)
    {
        if (itemsFound >= 5 && !itemsFoundAchievement)
        {
            var message = "Loot finder, you found 5 items";
            DisplayAcheivement(message);
            itemsFoundAchievement = true;
        }
    }


    private bool damagegivenStag1, damagegivenStag2, damagegivenStag3;
    public void DamageGiven(int damageGiven)
    {
        if (damageGiven >= 100 && !damagegivenStag1)
        {
            var message = "Pain bringer, dealt your first 100 damage";
            DisplayAcheivement(message);
            damagegivenStag1 = true;
        }

        if (damageGiven >= 300 && !damagegivenStag2)
        {
            var message = "Zeds be deds, dealt over 300 damage";
            DisplayAcheivement(message);
            damagegivenStag2 = true;
        }

        if (damageGiven >= 600 && !damagegivenStag3)
        {
            var message = "Exterminator, dealt over 600 damage";
            DisplayAcheivement(message);
            damagegivenStag3 = true;
        }
    }

    public void DamageTaken(int damage)
    {
        var message = "Congrats you killed your first super Zombie";
        DisplayAcheivement(message);
    }



    private bool enemieskilledstage1, enemieskilledstage2, enemieskilledstage3;
    public void EnemiesKilled(int enemiesKilled)
    {
        if (enemiesKilled >= 1 && !enemieskilledstage1)
        {
            var message = "baby steps, killed your first zomvie";
            DisplayAcheivement(message);
            enemieskilledstage1 = true;
        }

        if (enemiesKilled >= 5 && !enemieskilledstage2)
        {
            var message = "Taste for blood, kille dover 5 zombies";
            DisplayAcheivement(message);
            enemieskilledstage2 = true;
        }

        if (enemiesKilled >= 10 && !enemieskilledstage3)
        {
            var message = "Vengence, killed over 10 zombies";
            DisplayAcheivement(message);
            enemieskilledstage3 = true;
        }
    }


    private bool killedFirstZombie;
    public void SuperZombieAchievement()
    {
        if (!killedFirstZombie)
        {
            var message = "Congrats you killed your first super Zombie";
            DisplayAcheivement(message);
            killedFirstZombie = true;
        }
    }




}
