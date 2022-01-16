using System;
using System.Collections;
using System.Collections.Generic;
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

    List<IItemTypes> itemTypes = new List<IItemTypes>();
    public int noOfUniqueItemsRequired = 5;

    public event Action OnLevelComplete;
    public event Action OnInventoryScreen;

    public event Action OnAddItemType;

    private void OnEnable()
    {

    }

    public void AddItem(GameObject pickup)
    {
        var type = pickup.GetComponent<IItemTypes>();
        itemTypes.Add(type);

        CheckObjectiveIsComplete();
    }

    private void CheckObjectiveIsComplete()
    {
        var uniqueItems = itemTypes.GroupBy(x => x.Name).Count();
        if (uniqueItems >= noOfUniqueItemsRequired)
        {
            OnLevelComplete?.Invoke();
        }
    }

    public void AllowCompleteLevel()
    {

    }
}
