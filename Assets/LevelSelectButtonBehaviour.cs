using UnityEngine;
using System.Collections;

public class LevelSelectButtonBehaviour : MonoBehaviour {

    public void OnClick()
    {
        GameManager.instance.LoadMainMenu();
    }
}
