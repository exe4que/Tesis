using UnityEngine;
using System.Collections;

public class PlayerDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        PoolMaster.Spawn("Particles", "explossionParticle", this.transform.position, this.transform.rotation);
        AudioManager.instance.PlaySound("Explosion");
        this.gameObject.SetActive(false);
    }
}
