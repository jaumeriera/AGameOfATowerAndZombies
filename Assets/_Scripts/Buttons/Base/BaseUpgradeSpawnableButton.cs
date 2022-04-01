using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgradeSpawnableButton : BaseUpgradeButton
{
    [SerializeField] BaseUpgradeButtonSpawnableScriptable _extendedSettings;
    private int currentItems;


    public override void Start()
    {
        base.Start();
        currentItems = _extendedSettings.startItems;
    }

    protected void UpgradeBonus()
    {
        int currentCost;
        if (_extendedSettings.maxAtTime > currentItems && PlayerPrefs.GetInt(currentLevelKey) % _extendedSettings.spawnEvery == 0)
        {
            print("spawnItem");
            currentItems += 1;
            // SpawnNewItem();
        }
        else
        {
            PlayerPrefs.SetFloat(currentBonusKey, PlayerPrefs.GetFloat(currentBonusKey) * _settings.bonusIncrement);
        }
        PlayerPrefs.SetInt(currentLevelKey, PlayerPrefs.GetInt(currentLevelKey) + 1);
        if (!isFree)
        {
            money.MinusMoney(PlayerPrefs.GetInt(currentCostKey));
        }
        currentCost = Mathf.RoundToInt(_settings.costIncrement * PlayerPrefs.GetInt(currentCostKey));
        PlayerPrefs.SetInt(currentCostKey, currentCost);
        costText.text = currentCost.ToString();

    }
}
