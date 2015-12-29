using UnityEngine;
using System.Collections;

public class HealTest : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		col.GetComponent<LifeController>().takeHeal(1);
	}
}
