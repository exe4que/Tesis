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
    Camera mainCamera;
    Image[] children;

    void Awake()
    {
        children = this.GetComponentsInChildren<Image>();
        thisSlider = this.GetComponent<Slider>();
        mainCamera = Camera.main;
    }
    
    void OnEnable()
    {
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

        Vector3 pointInCamera = mainCamera.WorldToViewportPoint(this.transform.position);
        bool val = pointInCamera.x > 0 && pointInCamera.x < 1 && pointInCamera.y > 0 && pointInCamera.y < 1 && pointInCamera.z > 0;
        children[0].enabled = val;
        children[1].enabled = val;

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
