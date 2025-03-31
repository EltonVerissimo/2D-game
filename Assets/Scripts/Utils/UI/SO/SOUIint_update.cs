using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOUIint_update : MonoBehaviour
{
    public SOint sOint;
    public TextMeshProUGUI uiTextValue;

    void Start()
    {
        uiTextValue.text = sOint.value.ToString();
    }
    
    void Update()
    {
        uiTextValue.text = sOint.value.ToString();
    }
}
