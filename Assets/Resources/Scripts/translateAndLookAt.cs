using UnityEngine;
using System.Collections;

public class translateAndLookAt : MonoBehaviour
{

    public float velocity = 5f;
    Vector3 direction, inputAxis;
    Rigidbody2D body;

    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //controller.Move(new Vector3(inputAxis.x, inputAxis.y, 0) * velocity * Time.deltaTime);
        body.velocity = new Vector2(inputAxis.x, inputAxis.y) * velocity * Time.deltaTime;
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
