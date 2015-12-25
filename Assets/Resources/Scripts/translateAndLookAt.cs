using UnityEngine;
using System.Collections;

public class translateAndLookAt : MonoBehaviour {

    public float velocity = 5f;
    Vector3 direction, inputAxis;

	void Update () {
        inputAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        this.transform.Translate(Vector3.right * inputAxis.x * velocity * Time.deltaTime, Space.World);
        this.transform.Translate(Vector3.up * inputAxis.y * velocity * Time.deltaTime, Space.World);
        direction = new Vector3(this.transform.position.x + inputAxis.x, this.transform.position.y + inputAxis.y, this.transform.position.z);
        LookAtTarget(direction);
	}

    private void LookAtTarget(Vector3 _target)
    {
        Vector3 diff = _target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
