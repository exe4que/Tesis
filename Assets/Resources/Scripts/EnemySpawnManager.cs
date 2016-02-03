using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{

    public int _maxEnemiesOnScreen = 5, _currentEnemies = 0, enemiesPerBase = 10;


    private int _enemyBases;

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

    void Start()
    {
        this._maxEnemiesOnScreen = this._enemyBases * this.enemiesPerBase;
    }


    public void OnEnemyDied()
    {
        this._currentEnemies--;
    }

    public void OnBaseDestroyed()
    {
        this._maxEnemiesOnScreen -= enemiesPerBase;
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

    

    public int enemyBases
    {
        get { return _enemyBases; }
        set { _enemyBases = value; }
    }
    

    public void EnemySpawned() {
        _currentEnemies++;
    }
}
