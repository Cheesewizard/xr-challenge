using System;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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


    public event Action OnLevelComplete;
    public event Action OnInventoryScreen;

    // Refers to the keys within the level
    private int[] requiredObjectiveIds = new int[] { 11, 6, 3};

    private void OnEnable()
    {
        CharacterManager.Instance.onAddItemToInventory += CheckObjectiveIsComplete;
    }

    private void OnDisable()
    {
        CharacterManager.Instance.onAddItemToInventory -= CheckObjectiveIsComplete;
    }


    /// <summary>
    /// Checks to see if 5 game objects have been collected. If true it fires an event that allows the game to be completed
    /// </summary>
    private void CheckObjectiveIsComplete()
    {
        for (int i = 0; i < requiredObjectiveIds.Length; i++)
        {
            if (!CharacterManager.Instance.Inventory.Contains(requiredObjectiveIds[i]))
            {
                // Not collected all the items needed
                return;
            }
        }

        OnLevelComplete?.Invoke();
    }
}
