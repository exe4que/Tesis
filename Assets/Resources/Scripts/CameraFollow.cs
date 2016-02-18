using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject target;

    void Update()
    {
        //float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, smoothTimeX);
        //float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, smoothTimeY);

        //transform.position = new Vector3(posX, posY, transform.position.z);
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }


}
