using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelWarpManager : MonoBehaviour {

    private int selectedLevel = 0;
    public Button warpButton;

    private static LevelWarpManager _instance;

    public static LevelWarpManager instance
    {
        get
        {
            return _instance;
        }
    }
	void Awake () {
        _instance = this;
	}
	
	// Update is called once per frame
	public void Reset() {
        selectedLevel = 0;
        warpButton.interactable = false;
	}

    public void SelectLevel(int _value)
    {
        selectedLevel = _value;
        warpButton.interactable = true;
    }

    public void Warp()
    {
        Application.LoadLevel(selectedLevel);
    }
}
