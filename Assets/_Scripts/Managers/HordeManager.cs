using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [SerializeField] HordeScriptable _settings;
    [SerializeField] SpawnerManager spawner;

    private float bonusSpeed;
    private float bonusLife;
    private float bonusValue;
    private float spawnTime;
    private int quantity;

    private void Start()
    {
        bonusSpeed = _settings.startSpeed;
        bonusLife = _settings.startLife;
        bonusValue = _settings.startValue;
        quantity = _settings.startQuantity;
        spawnTime = _settings.startSpawnTime;
    }

    private void Update()
    {
        if(GameManager.gameStatus == GameManager.Status.WaitingToStart)
        {
            spawner.StartHorde(bonusSpeed, bonusLife, bonusValue, spawnTime, quantity);
            updateBonusesForNextHorde();
        }
    }

    private void updateBonusesForNextHorde()
    {
        bonusSpeed *= _settings.bonusNextHordeSpeed;
        bonusLife *= _settings.bonusNextHordeLife;
        bonusValue *= _settings.bonusNextHordeValue;
        quantity = Mathf.RoundToInt(_settings.bonusNextHordeQuantity * quantity);
        if (spawnTime > _settings.minSpawnTime)
        {
            spawnTime -= _settings.decrementSpawnTime;
        }
    }
}
