using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action onKill;
    public float delayToKill = 0;
    public int startLife = 10;
    public bool destroyOnKill = false;
    public int _currentLife;
    private bool _isDead = false;

    public FlashColor flashColor;

    private void Awake()
    {
        Init();
        if(flashColor == null)
        {
            flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            kill();
        }

        if(flashColor != null)
        {
            flashColor.flash();
        }
    }

    private void kill()
    {
        _isDead = true;

        if(destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
        onKill?.Invoke();
    }
}
