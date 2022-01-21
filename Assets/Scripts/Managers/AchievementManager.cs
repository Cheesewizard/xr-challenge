using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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





    private IEnumerator DisplayAcheivement(string message)
    {
        var formatted = $"{scoreHeader} \r\n {message}";
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


    private bool itemsFoundAchievement1, itemsFoundAchievement2, itemsFoundAchievemen3, itemsFoundAchievemen4;
    private void ItemsFound(int itemsFound)
    {
        if (itemsFound >= 5 && !itemsFoundAchievement1)
        {
            var message = "Loot finder, you found 5 items";
            StartCoroutine(DisplayAcheivement(message));
            itemsFoundAchievement1 = true;
        }

        if (itemsFound >= 10 && !itemsFoundAchievement2)
        {
            var message = "Day Job, you found 10 items";
            StartCoroutine(DisplayAcheivement(message));
            itemsFoundAchievement2 = true;
        }

        if (itemsFound >= 20 && !itemsFoundAchievemen3)
        {
            var message = "Metal Detector, you found 20 items";
            StartCoroutine(DisplayAcheivement(message));
            itemsFoundAchievemen3 = true;
        }

        if (itemsFound >= 30 && !itemsFoundAchievemen4)
        {
            var message = "gold Digger, you found 30 items";
            StartCoroutine(DisplayAcheivement(message));
            itemsFoundAchievemen4 = true;
        }

        if (itemsFound >= 50 && !itemsFoundAchievemen4)
        {
            var message = "Amazon, you found 50 items";
            StartCoroutine(DisplayAcheivement(message));
            itemsFoundAchievemen4 = true;
        }
    }


    private bool damagegivenStag1, damagegivenStag2, damagegivenStag3, damagegivenStag4, damagegivenStag5;
    public void DamageGiven(int damageGiven)
    {
        if (damageGiven >= 100 && !damagegivenStag1)
        {
            var message = "Pain bringer, dealt your first 100 damage";
            StartCoroutine(DisplayAcheivement(message));
            damagegivenStag1 = true;
        }

        if (damageGiven >= 600 && !damagegivenStag2)
        {
            var message = "Zeds be deds, dealt over 600 damage";
            StartCoroutine(DisplayAcheivement(message));
            damagegivenStag2 = true;
        }

        if (damageGiven >= 1200 && !damagegivenStag3)
        {
            var message = "Exterminator, dealt over 1200 damage";
            StartCoroutine(DisplayAcheivement(message));
            damagegivenStag3 = true;
        }

        if (damageGiven >= 2000 && !damagegivenStag4)
        {
            var message = "Collosal , dealt over 2000 damage";
            StartCoroutine(DisplayAcheivement(message));
            damagegivenStag4 = true;
        }

        if (damageGiven >= 3000 && !damagegivenStag5)
        {
            var message = "Saint , dealt over 3000 damage";
            StartCoroutine(DisplayAcheivement(message));
            damagegivenStag5 = true;
        }
    }

    private bool damgeTakenStage1, damgeTakenStage2, damgeTakenStage3;
    public void DamageTaken(int damage)
    {
        if (damage >= 25 && !damgeTakenStage1)
        {
            var message = "Its just a scratch, taken 25 damage";
            StartCoroutine(DisplayAcheivement(message));
            damgeTakenStage1 = true;
        }

        if (damage >= 50 && !damgeTakenStage2)
        {
            var message = "Halfway dead, taken 50 damage";
            StartCoroutine(DisplayAcheivement(message));
            damgeTakenStage2 = true;
        }

        if (damage >= 75 && !damgeTakenStage3)
        {
            var message = "Want Brains, taken 75 damage";
            StartCoroutine(DisplayAcheivement(message));
            damgeTakenStage3 = true;
        }
    }



    private bool enemieskilledstage1, enemieskilledstage2, enemieskilledstage3, enemieskilledstage4, enemieskilledstage5, enemieskilledstage6;
    public void EnemiesKilled(int enemiesKilled)
    {
        if (enemiesKilled >= 1 && !enemieskilledstage1)
        {
            var message = "Baby steps, killed your first zombie";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage1 = true;
        }

        if (enemiesKilled >= 5 && !enemieskilledstage2)
        {
            var message = "Taste for blood, killed over 5 zombies";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage2 = true;
        }

        if (enemiesKilled >= 10 && !enemieskilledstage3)
        {
            var message = "Vengence, killed over 10 zombies";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage3 = true;
        }

        if (enemiesKilled >= 20 && !enemieskilledstage4)
        {
            var message = "Stay Dead, killed over 20 zombies";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage4 = true;
        }

        if (enemiesKilled >= 30 && !enemieskilledstage5)
        {
            var message = "RIP, killed over 30 zombies";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage5 = true;
        }

        if (enemiesKilled >= 50 && !enemieskilledstage6)
        {
            var message = "RIP, killed over 50 zombies";
            StartCoroutine(DisplayAcheivement(message));
            enemieskilledstage6 = true;
        }
    }


    private bool killedFirstZombie;
    public void SuperZombieAchievement()
    {
        if (!killedFirstZombie)
        {
            var message = "Congrats you killed your first super zombie";
            StartCoroutine(DisplayAcheivement(message));
            killedFirstZombie = true;
        }
    }




}
