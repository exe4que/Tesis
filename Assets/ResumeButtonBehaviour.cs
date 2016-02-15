using UnityEngine;
using System.Collections;

public class ResumeButtonBehaviour : MonoBehaviour {

    public void OnClick()
    {
        GameManager.instance.Resume();
    }
}
