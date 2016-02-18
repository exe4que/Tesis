using UnityEngine;
using System.Collections;

public class Weapon_FlameThrower : MonoBehaviour, IFireable {

    private float lastShootTime = 0;

    [Range(0f, 50f)]
    public int fireRatePlayer = 10, fireRateBot = 5;
    [Range(0f, 1f)]
    public float bulletTraceSpacing = 0.1f;
    [Range(0f, 90f)]
    public int bulletTraceAngle = 30;

    private int fireRate;
    private string bulletName;
    public void OnShoot(LayerMask _validTarget, bool _isBot)
    {
        fireRate = _isBot ? fireRateBot : fireRatePlayer;
        bulletName = "FireParticle";
        if (Time.time >= lastShootTime + 1f / fireRate)
        {
            GameObject _bullet;
            AudioManager.instance.PlaySound("Fire");
            _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f + this.transform.right * bulletTraceSpacing, this.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-bulletTraceAngle, bulletTraceAngle)));
            _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f + this.transform.right * -bulletTraceSpacing, this.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-bulletTraceAngle, bulletTraceAngle)));
            _bullet = PoolMaster.SpawnReference("Projectiles", bulletName, this.transform.position + this.transform.up * 0.4f + this.transform.right * -bulletTraceSpacing, this.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-bulletTraceAngle, bulletTraceAngle)));
            lastShootTime = Time.time;
        }
    }
}
