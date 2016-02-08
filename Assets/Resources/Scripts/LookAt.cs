using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
	public bool isBot = false, followMouse = false;
	public Transform targetObject;
	public Vector3 targetPosition;

	void Update ()
	{
		if (!isBot) {
            targetPosition = InputManager.instance.GetFireVector();
            targetPosition += InputManager.instance.touchInput ? this.transform.position : Vector3.zero;
		}
		LookAtTarget (targetPosition);
	}

	private void LookAtTarget (Vector3 _target)
	{
		Vector3 diff = _target - transform.position;
		diff.Normalize ();

		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);
	}
}
