using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour, IBullet
{
    public int bulletType = 0;
    public float velocity = 10f;
    Vector3 hitPoint, initPoint, lastPoint;
    RaycastHit2D hit;
    float distance, maxDistance = 15f, lastRaycastTime = -1, raycastRate = 15f;



    public LayerMask whatToHit;
    void OnEnable()
    {
        initPoint = this.transform.position;
        lastPoint = this.transform.position + this.transform.up * 15f;
    }

    int cont = 0;
    private void PerformLinecast()
    {
        if (Time.time >= lastRaycastTime + 1f/raycastRate)
        {
            hit = Physics2D.Linecast(this.transform.position, this.transform.position + this.transform.up * 1f, whatToHit);
            lastRaycastTime = Time.time;
            //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.up * 1f, Color.red);
            //Debug.DrawLine(this.transform.position, lastPoint, Color.cyan);
        }
        
        hitPoint = hit.collider != null ? new Vector3(hit.point.x, hit.point.y) : lastPoint;
        distance = Vector3.Distance(this.transform.position, hitPoint);
        cont++;
    }
    void Update()
    {
        PerformLinecast();
        CheckColision(0.1f);
        //AdjustScale();
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    
    private void CheckColision(float _tolerance)
    {
        if (distance <= _tolerance)
        {
            this.gameObject.SetActive(false);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Idamageable>().Piew(bulletType);
                PoolMaster.Spawn("Particles", "bulletEffectPart", this.transform.position, LookAtTarget(initPoint));
            }
        }
    }

    private Quaternion LookAtTarget(Vector3 _target)
    {
        Vector3 diff = _target - transform.position;
        diff.Normalize();

        float rot_x = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(rot_x - 90f, 90f, 0);

        return rot;
    }

    float allDistance = -1;
    private void AdjustScale()
    {
        this.allDistance = allDistance == -1 ? Vector3.Distance(this.initPoint, this.lastPoint) : allDistance;
        float currentDistance = Vector3.Distance(this.transform.position, lastPoint);
        float scale = currentDistance / this.allDistance;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    public void SetValidTarget(LayerMask _validTarget)
    {
        whatToHit = _validTarget;
    }
}
