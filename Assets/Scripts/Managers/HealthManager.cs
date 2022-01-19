using System;
using System.Collections;
using System.Collections.Generic;
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
    public int PlayerHealth = 100;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorEventManager.Instance.OnEnemyAttack += TakeDamage;
        healthText.text = $"{scoreHeader}: {PlayerHealth}";
    }

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

        healthText.text = $"{scoreHeader}: {PlayerHealth}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
