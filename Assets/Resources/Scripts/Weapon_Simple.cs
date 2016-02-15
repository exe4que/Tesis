using UnityEngine;
using System.Collections;

public class Weapon_Simple : MonoBehaviour, IFireable {
    [Range(0f, 50f)]
    public int fireRatePlayer= 12, fireRateBot = 3;
    private float lastShootTime = 0;
    private int fireRate;
    private string bulletName;

    public void OnShoot(LayerMask _validTarget, bool _isBot)
    {
        fireRate = _isBot ? fireRateBot : fireRatePlayer;
        bulletName = _isBot ? "EnemyBullet" : "Bullet";
        if (Time.time >= lastShootTime + 1f / fireRate) {
            GameObject _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f, this.transform.rotation);
            AudioManager.instance.PlaySound("Shoot");
            _bullet.GetComponent<IBullet>().SetValidTarget(_validTarget);
            lastShootTime = Time.time;
        }
    }
}
