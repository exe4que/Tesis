using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform targetObject;

	void Update () {
        if(targetObject != null)
        this.transform.position = new Vector3(targetObject.position.x, targetObject.position.y, this.transform.position.z);
	}
}
