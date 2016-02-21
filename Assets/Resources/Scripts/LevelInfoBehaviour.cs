using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfoBehaviour : MonoBehaviour {

    private GameObject[] stars;
    public Text levelName;

	void Awake () {
        stars = GameObject.FindGameObjectsWithTag("Star");
	}

    void OnEnable()
    {
        string levelID = LevelWarpManager.instance.selectedLevel;
        levelName.text = levelID;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        int cant = PlayerPrefs.GetInt(levelID+ " - Stars");
        Debug.Log(levelID + " - Stars = " + cant + " Stars.");
        for (int i = 0; i < cant; i++)
        {
            stars[i].SetActive(true);
        }

    }
}
