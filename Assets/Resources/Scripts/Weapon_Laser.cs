using UnityEngine;
using System.Collections;

public class Weapon_Laser : MonoBehaviour, IFireable {
    [Range(0f, 50f)]
    public float fireRatePlayer = 1f, fireRateBot = 0.3f, fireRange = 10f;
    public Material laserMaterial;
    private float fireRate;
    private LineRenderer lineRenderer;

    float lastShootTime = 0f;

    void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.material = laserMaterial;
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetColors(Color.cyan, Color.white);
    }

    public void OnShoot(LayerMask _validTarget, bool _isBot)
    {
        fireRate = _isBot ? fireRateBot : fireRatePlayer;
        if (Time.time >= lastShootTime + 1f / fireRate)
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(this.transform.position, this.transform.up * fireRange, _validTarget);
            for (int i = 0; i < hits.Length; i++)
            {
                //hits[i].collider.GetComponent<Idamageable>().Piew(1);
                hits[i].collider.SendMessage("Piew", 1);
            }

            //lineRenderer.SetPosition(0, this.transform.position);
            //lineRenderer.SetPosition(1, this.transform.up * fireRange);
            lastShootTime = Time.time;
        }
    }
}
