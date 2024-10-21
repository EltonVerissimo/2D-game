using System;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using TMPro;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    public int coins;
    public TMP_Text uiTextCoins;
    public Action onAddCoins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        UpdateUI();
    }

    public void addCoins(int amount = 1)
    {
        coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        uiTextCoins.text = "X" + coins.ToString();
    }
}
