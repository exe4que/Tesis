using UnityEngine;
using System.Collections;

public class EnemyDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        PoolMaster.Spawn("Particles", "explossionParticle", this.transform.position, this.transform.rotation);
        AudioManager.instance.PlaySound("Explosion");
        EnemySpawnManager.instance.OnEnemyDied();
        this.gameObject.SetActive(false);
    }
}
