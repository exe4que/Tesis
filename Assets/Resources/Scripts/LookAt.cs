using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public bool isBot = false, followMouse = false;
    public Transform targetObject;
    public Vector3 targetPosition;

    public void AimAt()
    {
        if (!isBot)
        {
            targetPosition = InputManager.instance.GetFireVector();
            //targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition += InputManager.instance.touchInput ? this.transform.position : Vector3.zero;
            if (targetPosition == transform.position)
            {
                targetPosition = this.transform.position + this.transform.up;
            }

        }
        LookAtTarget(targetPosition);

    }

    private void LookAtTarget(Vector3 _target)
    {
        Vector3 diff = _target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void ResetRotation()
    {
        this.transform.rotation = new Quaternion();
    }
}
