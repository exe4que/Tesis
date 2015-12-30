using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	public bool isBot, fireButton;
	public float bulletsPerSecond = 20;
	float lastShootTime = 0;

	void Update ()
	{
		if (!isBot) {
			fireButton = Input.GetButton("Fire1");
		}
		if (fireButton) {
			OnShoot();
		}
	}

	void OnShoot ()
	{
		if (Time.time >= lastShootTime + 1 / bulletsPerSecond) {
			PoolMaster.Spawn ("Projectiles", "Bullet", this.transform.position + this.transform.up * 0.4f, this.transform.rotation);
			lastShootTime = Time.time;
		}
	}
}
