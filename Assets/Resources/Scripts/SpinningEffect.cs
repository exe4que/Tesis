using UnityEngine;
using System.Collections;

public class SpinningEffect : MonoBehaviour {

    public AnimationCurve curve;
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = new Vector3(curve.Evaluate(Time.time), 1f, 1f);
	}
}
