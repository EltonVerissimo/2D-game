using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ebac.Core.Singleton;

public class UIGameManager : Singleton<UIGameManager>
{
    public TextMeshProUGUI uiTextCoins;

    public static void UpdateTextCoins(string s)
    {
        Instance.uiTextCoins.text = s;
    }
}
