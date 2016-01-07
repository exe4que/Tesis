using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public bool followPlayer = true;
    public float YOffset = 0.5f;
    public bool isSmooth = false;
    public float smoothValue = 0.5f;
    public bool hasDamageIndicator = false;
    public float damageIndicatorDelay = 2f;
    public Slider damageSliderIndicator;
    public Transform target;
    LifeController lifeController;
    Slider thisSlider;
    float storedRealLife, timeOfLastImpact;

    void OnEnable()
    {
        thisSlider = this.GetComponent<Slider>();
        if (target != null)
        {
            lifeController = target.GetComponent<LifeController>();
            thisSlider.value = storedRealLife = lifeController.realLife;
        }
        //Debug.Log("thisSlider.value = " + thisSlider.value);
    }

    void Update()
    {
        if (!this.target.gameObject.activeSelf && followPlayer)
        {
            PoolMaster.Despawn(this.gameObject);
        }
        else
        {
            if (followPlayer)
                this.transform.position = target.position + Vector3.up * YOffset;
            ModifyValue(isSmooth, smoothValue);
        }
    }


    private void ModifyValue(bool _isSmooth = false, float _smoothValue = 0.5f)
    {
        if (_isSmooth)
        {
            thisSlider.value = Mathf.Lerp(thisSlider.value, lifeController.realLife, _smoothValue * Time.deltaTime);
        }
        else
        {
            thisSlider.value = lifeController.realLife;
        }
        if (hasDamageIndicator)
        {
            if (storedRealLife != lifeController.realLife)
            {
                storedRealLife = lifeController.realLife;
                timeOfLastImpact = Time.time;
            }
            if (Time.time >= timeOfLastImpact + damageIndicatorDelay)
            {
                damageSliderIndicator.value = Mathf.Lerp(damageSliderIndicator.value, lifeController.realLife, smoothValue * Time.deltaTime);
            }
        }
    }



    public void SetTarget(Transform _target)
    {
        target = _target;
        lifeController = target.GetComponent<LifeController>();
    }
}
