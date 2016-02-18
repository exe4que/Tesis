using UnityEngine;
using System.Collections;

public class Weapon_Double : MonoBehaviour, IFireable {

    private float lastShootTime = 0;

    [Range(0f, 50f)]
    public int fireRatePlayer = 10, fireRateBot = 5;
    [Range(0f, 1f)]
    public float bulletTraceSpacing = 10;
    [Range(0f, 90f)]
    public int bulletTraceAngle = 0;

    private int fireRate;
    private string bulletName;

    public void OnShoot(LayerMask _validTarget, bool _isBot)
    {
        fireRate = _isBot ? fireRateBot : fireRatePlayer;
        bulletName = _isBot ? "EnemyBullet" : "Bullet";
        if (Time.time >= lastShootTime + 1f / fireRate)
        {
            GameObject _bullet;
            AudioManager.instance.PlaySound("Shoot");
            _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f + this.transform.right * bulletTraceSpacing, this.transform.rotation * Quaternion.Euler(0, 0, -bulletTraceAngle));
            _bullet.GetComponent<IBullet>().SetValidTarget(_validTarget);
            _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f + this.transform.right * -bulletTraceSpacing, this.transform.rotation * Quaternion.Euler(0, 0, bulletTraceAngle));
            _bullet.GetComponent<IBullet>().SetValidTarget(_validTarget);
            lastShootTime = Time.time;
        }
    }
}
