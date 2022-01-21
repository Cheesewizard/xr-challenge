using System.Linq;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public bool requireKeyCard;
    public Material doorUnlocked;

    [SerializeField]
    private int[] requiredIds;
    private bool isDoorUnlocked;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (requireKeyCard && !isDoorUnlocked)
            {
                for (int i = 0; i < requiredIds.Length; i++)
                {
                    // Check if keycard exists within the inventory
                    if (!CharacterManager.Instance.Inventory.Any(x => x == requiredIds[i]))
                    {
                        return;
                    }
                }

                SetDoorToUnlocked();
            }

           EventManager.Instance.DoorOpenTriggerEnter(requiredIds[0]);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (requireKeyCard && !isDoorUnlocked)
            {
                for (int i = 0; i < requiredIds.Length; i++)
                {
                    // Check if keycard exists within the inventory
                    if (!CharacterManager.Instance.Inventory.Any(x => x == requiredIds[i]))
                    {
                        return;
                    }
                }
            }

            EventManager.Instance.DoorOpenTriggerExit(requiredIds[0]);
        }
    }

    private void SetDoorToUnlocked()
    {
        // We have a keycard
        isDoorUnlocked = true;

        // Ugly, but other methods didnt work
        var child = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0);
        var rend = child.gameObject.GetComponent<Renderer>();
        rend.material = doorUnlocked;
    }
}