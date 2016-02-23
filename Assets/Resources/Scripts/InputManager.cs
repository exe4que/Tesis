using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public bool touchInput = false;
    public Joystick joystickLeft;
    public Joystick joystickRight;
    private Camera mainCamera;

    private static InputManager _instance;

    public static InputManager instance
    {
        get
        {
            return _instance;
        }
    }


	void Awake () {
        joystickLeft.gameObject.SetActive(touchInput);
        joystickRight.gameObject.SetActive(touchInput);
        mainCamera = Camera.main;
        _instance = this;
	}

    public float GetAxis(string _axisName)
    {
        float ret = 0;
        switch (_axisName)
        {
            case "Horizontal":
                ret = (touchInput) ? joystickLeft.Horizontal() : Input.GetAxis(_axisName);
                break;
            case "Vertical":
                ret = (touchInput) ? joystickLeft.Vertical() : Input.GetAxis(_axisName);
                break;
        }
        return ret;
    }

    public bool GetButton(string _buttonName)
    {
        bool ret = false;
        switch (_buttonName)
        {
            case "Fire1":
                ret = touchInput ? (GetFireVector().magnitude != 0) : Input.GetButton(_buttonName);
                break;
        }
        return ret;
    }

    public Vector3 GetFireVector()
    {
        Vector3 ret = (touchInput) ? joystickRight.GetDirection() : mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return ret;
    }
}
