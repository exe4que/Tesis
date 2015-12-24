using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public bool followMouse = false;
    public GameObject targetObject;

    void Start()
    {

    }
    void Update()
    {
        if (followMouse)
        {
            LookAtTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            LookAtTarget(targetObject.transform.position);
        }
    }

    private void LookAtTarget(Vector3 _target)
    {
        Vector3 diff = _target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
