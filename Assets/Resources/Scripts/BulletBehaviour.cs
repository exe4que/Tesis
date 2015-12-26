using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{

    Vector3 hitPoint, originalPosition, lastPoint;
    RaycastHit2D hit;
    float distance, maxDistance = 5f, velocity = 10f;
    void OnEnable()
    {
        originalPosition = this.transform.position;
        lastPoint = this.transform.position + this.transform.up * maxDistance;
        hit = Physics2D.Linecast(this.transform.position, lastPoint);
        hitPoint = hit.collider != null ? new Vector3(hit.point.x, hit.point.y) : lastPoint;
        distance = Vector3.Distance(this.transform.position, hitPoint);
    }
    void Update()
    {
        //Debug.DrawLine(this.transform.position, lastPoint, Color.red);
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
        //this.gameObject.SetActive(!(Vector3.SqrMagnitude(this.transform.position - hitPoint) < 0.01f));
        if (Vector3.Distance(this.transform.position, originalPosition) >= distance)
        {
            this.gameObject.SetActive(false);
            if (hit.collider != null)
            {
                hit.collider.SendMessage("Piew", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
