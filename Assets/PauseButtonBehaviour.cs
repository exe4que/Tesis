using UnityEngine;
using System.Collections;

public class PauseButtonBehaviour : MonoBehaviour {

    public void OnClick()
    {
        GameManager.instance.Pause();
    }
}
