using UnityEngine;
using System.Collections;

public class EnemyDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        EnemySpawnManager.instance.OnEnemyDied();
        this.gameObject.SetActive(false);
    }
}
