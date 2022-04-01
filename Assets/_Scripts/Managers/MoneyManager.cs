using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private int totalMoney;
    [SerializeField] Text moneyText;

    private void Start()
    {
        totalMoney = 0;
        moneyText.text = totalMoney.ToString();
    }

    public void AddMoney(int ammount)
    {
        totalMoney += ammount;
        UpdateMoneyText();
    }

    public void MinusMoney(int ammount)
    {
        totalMoney -= ammount;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = totalMoney.ToString();
    }

    public bool HaveEnoughMoney(int cost)
    {
        return cost <= totalMoney;
    }
}
