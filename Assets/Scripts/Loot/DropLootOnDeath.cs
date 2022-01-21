using UnityEngine;

public class DropLootOnDeath : MonoBehaviour
{
    public GameObject[] lootDrops;

    public void DropLoot()
    {
        // I should weight it more towards ammo in the future

        var random = Random.Range(0, lootDrops.Length);
        Instantiate(lootDrops[random], gameObject.transform.position, gameObject.transform.rotation);
    }
}
