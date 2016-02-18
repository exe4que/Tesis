using UnityEngine;
using System.Collections;

public class EnemyDeathController : MonoBehaviour, IDestroyable {

    bool enabled = true;

    void OnEnable()
    {
        enabled = true;
    }

    public void OnDeath()
    {
        if (enabled)
        {
            enabled = false;
            PoolMaster.Spawn("Particles", "explossionParticle", this.transform.position, this.transform.rotation);
            if ((int)Random.Range(0, 4) == 0) PoolMaster.SpawnRandom("PowerUps", this.transform.position);
            GameManager.instance.AddPoints();
            AudioManager.instance.PlaySound("Explosion");
            EnemySpawnManager.instance.OnEnemyDied();
            this.gameObject.SetActive(false);
        }
        
    }
}
