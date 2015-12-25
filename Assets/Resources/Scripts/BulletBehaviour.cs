using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{

    Vector3 hitPoint, originalPosition;
    float distance, maxDistance = 8f, velocity = 10f;
    void OnEnable()
    {
        originalPosition = this.transform.position;
        Vector3 lastPoint = this.transform.position + this.transform.up * maxDistance;
        RaycastHit2D hit = Physics2D.Linecast(this.transform.position, lastPoint);
        hitPoint = hit.collider != null ? new Vector3(hit.point.x, hit.point.y) : lastPoint;
        distance = Vector3.Distance(this.transform.position, hitPoint);
    }
    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
        //this.gameObject.SetActive(!(Vector3.SqrMagnitude(this.transform.position - hitPoint) < 0.01f));
        this.gameObject.SetActive(Vector3.Distance(this.transform.position, originalPosition) < distance);
    }
}
