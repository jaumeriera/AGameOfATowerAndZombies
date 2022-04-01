using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [SerializeField] HordeScriptable _settings;
    [SerializeField] SpawnerManager spawner;

    public string bonusSpeedKey;
    public string bonusLifeKey;
    public string bonusValueKey;
    public string spawnTimeKey;
    public string quantityKey;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(bonusSpeedKey) || !PlayerPrefs.HasKey(bonusLifeKey) || 
            !PlayerPrefs.HasKey(bonusValueKey) || !PlayerPrefs.HasKey(spawnTimeKey) ||
            !PlayerPrefs.HasKey(quantityKey))
        {
            PlayerPrefs.SetFloat(bonusSpeedKey, _settings.startSpeed);
            PlayerPrefs.SetFloat(bonusLifeKey, _settings.startLife);
            PlayerPrefs.SetFloat(bonusValueKey, _settings.startValue);
            PlayerPrefs.SetFloat(spawnTimeKey, _settings.startSpawnTime);
            PlayerPrefs.SetInt(quantityKey, _settings.startQuantity);
        }
    }

    private void Update()
    {
        if(GameManager.gameStatus == GameManager.Status.WaitingToStart)
        {
            spawner.StartHorde(
                PlayerPrefs.GetFloat(bonusSpeedKey),
                PlayerPrefs.GetFloat(bonusLifeKey),
                PlayerPrefs.GetFloat(bonusValueKey),
                PlayerPrefs.GetFloat(spawnTimeKey),
                PlayerPrefs.GetInt(quantityKey)
            );
            updateBonusesForNextHorde();
        }
    }

    private void updateBonusesForNextHorde()
    {
        int quantity;
        PlayerPrefs.SetFloat(bonusSpeedKey, PlayerPrefs.GetFloat(bonusSpeedKey) * _settings.bonusNextHordeSpeed);
        PlayerPrefs.SetFloat(bonusLifeKey, PlayerPrefs.GetFloat(bonusLifeKey) * _settings.bonusNextHordeLife);
        PlayerPrefs.SetFloat(bonusValueKey, PlayerPrefs.GetFloat(bonusValueKey) * _settings.bonusNextHordeValue);
        quantity = Mathf.RoundToInt(_settings.bonusNextHordeQuantity * PlayerPrefs.GetInt(quantityKey));
        if (PlayerPrefs.GetFloat(spawnTimeKey) > _settings.minSpawnTime)
        {
            PlayerPrefs.SetFloat(spawnTimeKey, PlayerPrefs.GetFloat(spawnTimeKey) - _settings.decrementSpawnTime);
        }
    }
}
