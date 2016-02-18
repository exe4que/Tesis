using UnityEngine;
using System.Collections;
using VectorExtensionMethods;

[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(LookAt))]
[RequireComponent(typeof(CircleCollider2D))]
public class BotAimingAndShootingController : MonoBehaviour
{
    public float aimingUnitsError = 1f;
    Shoot shootScript;
    LookAt lookAtScript;
    Vector3 targetPosition;
    bool fireButton = false;
    float lastShootTime = 0f;


    void Awake()
    {
        targetPosition = this.transform.position + this.transform.up;
        shootScript = this.GetComponent<Shoot>();
        lookAtScript = this.GetComponent<LookAt>();

    }

    void Update()
    {
        shootScript.fireButton = fireButton;
        lookAtScript.targetPosition = targetPosition;

        if (Time.time >= lastShootTime + 0.5f)
        {
            targetPosition = this.transform.position + this.transform.up;
            fireButton = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        targetPosition = col.transform.position + Random.insideUnitCircle.ToVector3() * aimingUnitsError;
        fireButton = true;
        lastShootTime = Time.time;
    }
}
