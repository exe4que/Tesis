using UnityEngine;
using System.Collections;

public class translateAndLookAt : MonoBehaviour
{
    public bool isBot;
    public float velocity = 200f;
    [HideInInspector]
    public Vector3 inputAxis;
    Vector3 direction;
    Rigidbody2D body;
    public CircleCollider2D thisCollider;
    BotMovementController controller;
    LookAt helmet;
    Shoot sameHelmet;
    //public LayerMask whatToCollideWith;

    void Awake()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.thisCollider = this.GetComponent<CircleCollider2D>();
        this.helmet = this.GetComponentInChildren<LookAt>();
        this.sameHelmet = this.GetComponentInChildren<Shoot>();
        if (isBot)
        {

            if ((controller = this.GetComponent<BotMovementController>()) == null)
            {
                Debug.LogError("BotMovementController is necessary");
            }
        }
    }

    void Update()
    {
        helmet.AimAt();
        sameHelmet.ShootNow();
        MoveAndLookAt();

    }

    private void MoveAndLookAt()
    {
        if (!isBot)
        {
            this.inputAxis = new Vector3(InputManager.instance.GetAxis("Horizontal"), InputManager.instance.GetAxis("Vertical"), 0);
        }
        else
        {
            if (controller == null)
            {
                Debug.LogError("BotMovementController is necessary");
            }
        }
        inputAxis.Normalize();
        //body.MovePosition(transform.position + new Vector3(inputAxis.x, inputAxis.y, 0) * velocity * Time.deltaTime);
        body.velocity = new Vector3(inputAxis.x, inputAxis.y, 0) * velocity * 0.01f;
        direction = new Vector3(this.transform.position.x + inputAxis.x, this.transform.position.y + inputAxis.y, this.transform.position.z);
        if (this.transform.position != direction)
        {
            LookAtTarget(direction);
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
