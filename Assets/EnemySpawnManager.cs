using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{

    public int maxEnemies = 20, maxEnemiesOnScreen = 5;
    float spawningDelay = 1f, lastSpawnTime = 0f;
    int currentEnemies = 0, enemyCont = 0;
    GameObject[] enemyBases;
    Transform foregroundCanvas;

    public static EnemySpawnManager _instance;

    public static EnemySpawnManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        this.enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        this.foregroundCanvas = GameObject.Find("ForegroundCanvas").transform;
        _instance = this;
    }

    void Update()
    {
        if (Time.time >= lastSpawnTime + spawningDelay)
        {
            if (currentEnemies < maxEnemiesOnScreen && enemyCont < maxEnemies)
            {
                Transform enemy = PoolMaster.SpawnReference("Entities", "Enemy", enemyBases[Random.Range(0, enemyBases.Length)].transform.position).transform;
                GameObject lifeBar = PoolMaster.SpawnReference("Entities", "LifeBar", enemyBases[Random.Range(0, enemyBases.Length)].transform.position);
                lifeBar.transform.SetParent(foregroundCanvas, false);
                lifeBar.GetComponent<LifeBarController>().SetTarget(enemy);
                currentEnemies++;
                enemyCont++;
                lastSpawnTime = Time.time;
                //Debug.Log("currentEnemies, maxEnemiesOnScreen = (" + currentEnemies + ", " + maxEnemiesOnScreen + ")");
            }
        }
    }

    public void OnEnemyDied()
    {
        currentEnemies--;
    }

}
