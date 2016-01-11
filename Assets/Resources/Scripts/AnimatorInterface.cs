using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class AnimatorInterface : MonoBehaviour, IAnimable {

    private IAnimable2 animatorScript;
    private Animator animator;

    public bool isScriptAnimated = false;

    void Awake()
    {
        if (isScriptAnimated)
        {
            if ((animatorScript = this.GetComponent<IAnimable2>()) == null)
                Debug.LogError(this.gameObject.name + " must have an 'IAnimable' delegated component if it's script-Animated");
        }
        else
        {
            if ((animator = this.GetComponent<Animator>()) == null)
                Debug.LogError(this.gameObject.name + " must have an 'Animator' component if it's not script-Animated");
        }
    }

    public void SetBool(string _param, bool _value)
    {
        if (isScriptAnimated)
            animatorScript.SetBool(_param, _value);
        else
            animator.SetBool(_param, _value);
    }

    public void SetFloat(string _param, float _value)
    {
        if (isScriptAnimated)
            animatorScript.SetFloat(_param, _value);
        else
            animator.SetFloat(_param, _value);
    }

    public void SetInt(string _param, int _value)
    {
        if (isScriptAnimated)
            animatorScript.SetInt(_param, _value);
        else
            animator.SetInteger(_param, _value);
    }
}
