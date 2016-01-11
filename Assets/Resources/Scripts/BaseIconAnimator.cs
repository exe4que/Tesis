using UnityEngine;
using System;
using UnityEngine.UI;

public class BaseIconAnimator : MonoBehaviour, IAnimable2 {

    public AnimationCurve curve;
    public Gradient gradient;
    public float timeScale = 1;
    bool _turningOff = false;
    float time = 0;
    Image thisImage;

    void Awake()
    {
        thisImage = this.GetComponent<Image>();
    }

    void Update()
    {
        if (_turningOff)
        {
            time += Time.deltaTime * timeScale;
            if (time <= 1)
                thisImage.color = gradient.Evaluate(curve.Evaluate((float)(time)));
            else
                _turningOff = false;
        }
    }

    public void SetBool(string _param, bool _value)
    {
        this.SendMessage(_param, _value);
    }

    public void SetFloat(string _param, float _value)
    {
        this.SendMessage(_param, _value);
    }

    public void SetInt(string _param, int _value)
    {
        this.SendMessage(_param, _value);
    }

    public void TurningOff(bool _value)
    {
        time = 0;
        _turningOff = _value;
    }
}
