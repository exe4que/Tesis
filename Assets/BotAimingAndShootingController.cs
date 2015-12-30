using UnityEngine;
using System.Collections;
using VectorExtensionMethods;

[RequireComponent (typeof(Shoot))]
[RequireComponent (typeof(LookAt))]
[RequireComponent (typeof(CircleCollider2D))]
public class BotAimingAndShootingController : MonoBehaviour
{
	public float aimingUnitsError = 1f;
	Shoot shootScript;
	LookAt lookAtScript;
	Vector3 targetPosition;
	bool fireButton = false;

	void Awake ()
	{
		targetPosition = transform.position + transform.parent.up;
		shootScript = this.GetComponent<Shoot>();
		lookAtScript = this.GetComponent<LookAt>();
	}

	void Update(){
		shootScript.fireButton = fireButton;
		lookAtScript.targetPosition = targetPosition;
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.name == "Player"){
			targetPosition = col.transform.position + Random.insideUnitCircle.ToVector3() * aimingUnitsError;
			fireButton = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		targetPosition = transform.position + transform.parent.up;
		fireButton = false;
	}
}
