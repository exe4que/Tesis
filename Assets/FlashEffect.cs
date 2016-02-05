using UnityEngine;
using System.Collections;

public class FlashEffect : MonoBehaviour, IAnimable2 {

    public Material flashMaterial, defaultMaterial;
    SpriteRenderer thisRenderer, ChildRenderer;

    void Awake()
    {
        thisRenderer = this.GetComponent<SpriteRenderer>();
        ChildRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
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

    public void isFlashing(bool _value)
    {
        if (_value)
        {
            thisRenderer.material = flashMaterial;
            ChildRenderer.material = flashMaterial;
            Invoke("Restore", 0.2f);
        }
    }

    private void Restore()
    {
        thisRenderer.material = defaultMaterial;
        ChildRenderer.material = defaultMaterial;
    }
}
