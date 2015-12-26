using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public float bulletsPerSecond = 20;
    float lastShootTime = 0;
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= lastShootTime + 1/bulletsPerSecond)
            {
                PoolMaster.Spawn("Projectiles", "Bullet", this.transform.position + this.transform.up * 0.5f, this.transform.rotation);
                lastShootTime = Time.time;
            }
        }
	}
}
