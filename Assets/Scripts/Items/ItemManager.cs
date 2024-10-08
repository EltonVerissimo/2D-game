using System;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{

    public int coins;
    public TMP_Text coinsText;
    public Action onAddCoins;

    private void Start()
    {
        Reset();
        setCoinsText(coins);
    }

    private void Reset()
    {
        coins = 0;
        setCoinsText(coins);
    }

    public void addCoins(int amount = 1)
    {
        coins += amount;
        setCoinsText(coins);
        onAddCoins?.Invoke();
    }

    private void setCoinsText(int value)
    {
        this.coinsText.text = "x" + value.ToString();
    }
}
