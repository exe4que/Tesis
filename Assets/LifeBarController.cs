using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
	public float YOffset = 0.5f;
	public Transform target;
	LifeController lifeController;
	Slider thisSlider;

	void Awake ()
	{
		lifeController = target.GetComponent<LifeController>();
		thisSlider = this.GetComponent<Slider>();
	}

	void Update ()
	{
		this.transform.position = target.position + Vector3.up * YOffset;
		thisSlider.value = lifeController.realLife;
	}
}
