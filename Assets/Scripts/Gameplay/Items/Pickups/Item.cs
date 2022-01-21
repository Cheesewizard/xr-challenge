using UnityEngine;

public class Item : ItemScore, IPickup
{
    [SerializeField]
    private new string name;
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
        }
    }
}
