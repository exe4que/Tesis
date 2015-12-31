using UnityEngine;
using System.Collections;

public class translateAndLookAt : MonoBehaviour
{
    public bool isBot;
    public float velocity = 200f;
    [HideInInspector]
    public Vector3 inputAxis;
    public bool isColliding = false;
    Vector3 direction;
    Rigidbody2D body;
    CircleCollider2D thisCollider;
    BotMovementController controller;

    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        thisCollider = this.GetComponent<CircleCollider2D>();
        if (isBot)
        {
            if ((controller = this.GetComponent<BotMovementController>()) != null)
            {
            }
            else {
                Debug.LogError("BotMovementController is necessary");
            }
        }
    }

    void Update()
    {

        isColliding = thisCollider.IsTouchingLayers();
        if (!isBot)
        {
            inputAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        else {
            if (controller == null)
            {
                Debug.LogError("BotMovementController is necessary");
            }
        }
        inputAxis.Normalize();
        //body.MovePosition(transform.position + new Vector3(inputAxis.x, inputAxis.y, 0) * velocity * Time.deltaTime);
        body.velocity = new Vector3(inputAxis.x, inputAxis.y, 0) * velocity * Time.deltaTime;
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

    //void OnCollisionStay2D(Collision2D col)
    //{
    //    isColliding = true;
    //}

    //void OnCollisionExit2D(Collision2D col)
    //{
    //    isColliding = false;
    //}
}
