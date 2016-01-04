using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public int bulletType = 0;
    public float velocity = 10f;
    Vector3 hitPoint, lastPoint;
    RaycastHit2D hit;
    float distance, maxDistance = 5f;
    public LayerMask whatToHit;
    void OnEnable()
    {
        lastPoint = this.transform.position + this.transform.up * maxDistance;
        PerformLinecast();
    }

    private void PerformLinecast()
    {
        hit = Physics2D.Linecast(this.transform.position, lastPoint, whatToHit);
        hitPoint = hit.collider != null ? new Vector3(hit.point.x, hit.point.y) : lastPoint;
        distance = Vector3.Distance(this.transform.position, hitPoint);
    }
    void Update()
    {
        //Debug.DrawLine(this.transform.position, lastPoint, Color.red);
        //Debug.DrawLine(this.transform.position, hitPoint, Color.cyan);
        
        PerformLinecast();
        CheckColision(0.1f);
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
                PoolMaster.Spawn("Particles", "bulletEffectPart", this.transform.position, LookAtTarget(GameObject.Find("Player").transform.position));
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
}
