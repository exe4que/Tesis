using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public float YOffset = 0.5f;
    public Transform target;
    LifeController lifeController;
    Slider thisSlider;

    void OnEnable()
    {
        thisSlider = this.GetComponent<Slider>();
        lifeController = target.GetComponent<LifeController>();
        thisSlider.value = lifeController.realLife;
        //Debug.Log("thisSlider.value = " + thisSlider.value);
    }

    void Update()
    {
        if (!this.target.gameObject.activeSelf)
        {
            PoolMaster.Despawn(this.gameObject);
        }
        this.transform.position = target.position + Vector3.up * YOffset;
        thisSlider.value = lifeController.realLife;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        lifeController = target.GetComponent<LifeController>();
    }
}
