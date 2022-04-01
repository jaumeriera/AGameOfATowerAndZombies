using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class TowerCollider : MonoBehaviour
{
    [SerializeField] TowerHealthScriptable _settings;
    private HealthManager healthManager;
    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Start()
    {
        InitHealth(_settings.health);
    }

    private void InitHealth(float health)
    {
        healthManager.SetUp(health);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        print("die");
        // Call to game over view
        return;
    }

}
