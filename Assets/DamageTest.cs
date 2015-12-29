using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		col.GetComponent<LifeController>().takeDamage(1);
	}
}
