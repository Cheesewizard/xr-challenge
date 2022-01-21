using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance { get { return _instance; } }

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


    // Events
    public Action onAddItemToInventory;
    public Action<int> onPlayerTakeDamage;

    // Health
    public int PlayerHealth = 100;
    private bool isDead;

    // Ammo
    public int currentAmmoClip;
    public int totalAmmunition;

    /// <summary>
    /// gets and sets the Inventory items
    /// </summary>
    public List<int> Inventory = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        AnimatorEventManager.Instance.OnEnemyAttack += TakeDamage;
    }

    /// <summary>
    /// Character takes damage to their health equal to the damage passed in by the enemy
    /// </summary>
    /// <param name="enemy"></param>
    private void TakeDamage(Enemy enemy)
    {
        if (isDead)
        {
            return;
        }

        PlayerHealth -= enemy.Power;
        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            AnimatorEventManager.Instance.PlayerDeath();
            isDead = true;
        }

        onPlayerTakeDamage?.Invoke(PlayerHealth);
    }

    /// <summary>
    /// Adds an items to the game inventory. 
    /// If an item is an objective then the item is added to the inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Pickup item)
    {
        var key = item.GetComponent<IObjective>();
        if (key != null)
        {

            Inventory.Add(key.Id);
            onAddItemToInventory?.Invoke();
        }
    }
}
