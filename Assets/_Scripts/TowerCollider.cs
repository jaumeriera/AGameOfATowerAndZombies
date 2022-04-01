using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class TowerCollider : MonoBehaviour
{
    [SerializeField] TowerHealthScriptable _settings;
    private HealthManager healthManager;

    public string healthKey;
    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(healthKey))
        {
            InitHealth(PlayerPrefs.GetFloat(healthKey), healthKey);
        }else
        {
            InitHealth(_settings.health, healthKey);
        }
    }

    private void InitHealth(float health, string healthKey)
    {
        healthManager.SetUp(health, healthKey);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        print("die");
        // Call to game over view
        return;
    }

}
