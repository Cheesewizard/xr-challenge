using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private static HealthManager _instance;
    public static HealthManager Instance { get { return _instance; } }

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
    private string scoreHeader = "HEALTH";
    public TextMeshProUGUI healthText;


    private void Start()
    {
        // Set start health text
        var health = CharacterManager.Instance.PlayerHealth;
        healthText.text = $"{scoreHeader}: {health}";
    }

    private void OnEnable()
    {
        CharacterManager.Instance.onPlayerTakeDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        CharacterManager.Instance.onPlayerTakeDamage -= UpdateHealth;
    }

    private void UpdateHealth(int health)
    {
        healthText.text = $"{scoreHeader}: {health}";
    }
}
