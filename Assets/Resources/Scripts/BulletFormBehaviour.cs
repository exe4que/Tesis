using UnityEngine;
using System.Collections;

public class BulletFormBehaviour : MonoBehaviour {
	void Update () {
        this.gameObject.SetActive(this.transform.childCount != 0);
	}
}
