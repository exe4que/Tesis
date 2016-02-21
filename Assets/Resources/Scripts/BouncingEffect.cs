using UnityEngine;
using System.Collections;

public class BouncingEffect : MonoBehaviour {

    public AnimationCurve curve;
    [Range(1f,100f)]
    public float range = 1f;
    [Range(1f, 10f)]
    public float velocity = 1f;


    float clock = 0f;
    Vector3 initPos;


    void Awake()
    {
        initPos = this.transform.localPosition;
    }

	void Update () {
        clock += Time.deltaTime;
        this.transform.localPosition = new Vector3(initPos.x, initPos.y + curve.Evaluate(clock * velocity) * range, initPos.z);
	}
}
