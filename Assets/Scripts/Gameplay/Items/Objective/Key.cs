using UnityEngine;

public class Key : Objective, IPickup, IObjective
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

    [SerializeField]
    private int id;
    public int Id
    {
        get { return id; }
        set
        {
            id = value;
        }
    }

}
