using UnityEngine;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {

    Transform foregroundCanvas;
    float spawningDelay = 1f, lastSpawnTime = 0f;
	// Use this for initialization
	void Start () {
        this.foregroundCanvas = GameObject.Find("ForegroundCanvas").transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= lastSpawnTime + spawningDelay)
        {
            if (EnemySpawnManager.instance.currentEnemies < EnemySpawnManager.instance.maxEnemiesOnScreen)
            {
                SpawnBot();
            }
        }
	
	}

    private void SpawnBot()
    {
        EnemySpawnManager.instance.EnemySpawned();
        lastSpawnTime = Time.time;
        StartCoroutine(SpawnDelayed(1f));
    }

    IEnumerator SpawnDelayed(float i)
    {
        yield return new WaitForSeconds(i);
        Transform enemy = PoolMaster.SpawnReference("Entities", "Enemy", this.transform.position).transform;
        GameObject lifeBar = PoolMaster.SpawnReference("Entities", "LifeBar", enemy.position);
        lifeBar.transform.SetParent(foregroundCanvas, false);
        lifeBar.GetComponent<LifeBarController>().SetTarget(enemy);
    }

}
