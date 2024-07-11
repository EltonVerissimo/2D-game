using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;

    private GameObject _currentPlayer;

    public void Init()
    {

    }

    private void spawnPlayer()
    {
        
    }
}
