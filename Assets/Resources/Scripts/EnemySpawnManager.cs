using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{

    public int _maxEnemiesOnScreen = 5, _currentEnemies = 0;
    GameObject[] enemyBases;

    private static EnemySpawnManager _instance;

    public static EnemySpawnManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }



    public void OnEnemyDied()
    {
        _currentEnemies--;
    }

    /*
     * propiedades
     */

    public int maxEnemiesOnScreen
    {
        get
        {
            return _maxEnemiesOnScreen;
        }
        set
        {
            _maxEnemiesOnScreen = value;
        }
    }

    public int currentEnemies
    {
        get
        {
            return _currentEnemies;
        }
    }

    public void EnemySpawned() {
        _currentEnemies++;
    }
}
