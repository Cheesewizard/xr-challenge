using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private static AmmoManager _instance;
    public static AmmoManager Instance { get { return _instance; } }

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

    [SerializeField]
    private string scoreHeader = "Ammunition";
    public TextMeshProUGUI currentClipUi;
    public TextMeshProUGUI totalAmmoUi;


    public void UpdateCurrentAmmoClip(int currentAmmo)
    {
        currentClipUi.text = $"{scoreHeader}: {currentAmmo} / ";
    }


    public void UpdateTotalAmmo(int totalAmmo)
    {
        totalAmmoUi.text = $"{totalAmmo}";
    }

}
