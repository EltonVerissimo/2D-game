using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float delayToKill = 0;
    public int startLife = 10;
    public bool destroyOnKill = false;
    public int _currentLife;
    private bool _isDead = false;

    private void Awake()
    {
        Init();
    }

    void Start()
    {

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
    }

    private void kill()
    {
        _isDead = true;

        if(destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
    }
}
