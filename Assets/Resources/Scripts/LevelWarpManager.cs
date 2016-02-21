using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelWarpManager : MonoBehaviour
{

    private string _selectedLevel = "";
    public Button warpButton;
    public GameObject logo, levelInfo;
    public GameObject[] warpers;

    private static LevelWarpManager _instance;

    public static LevelWarpManager instance
    {
        get
        {
            return _instance;
        }
    }

    public string selectedLevel
    {
        get
        {
            return _selectedLevel;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        warpers = FindObsWithTag("Warper");
        InitWarpers();
    }

    GameObject[] FindObsWithTag(string tag)
    {
        GameObject[] foundObs = GameObject.FindGameObjectsWithTag(tag);
        Array.Sort(foundObs, CompareObNames);
        return foundObs;
    }


    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    private void InitWarpers()
    {
        for (int i = 0; i < warpers.Length; i++)
        {
            warpers[i].SetActive(false);
        }

        string maxLevelUnblocked = PlayerPrefs.GetString("MaxLevelUnblocked");
        if (maxLevelUnblocked == "" || maxLevelUnblocked == "Level0")
        {
            warpers[0].SetActive(true);
            PlayerPrefs.SetString("MaxLevelUnblocked", "Level0");
        }
        else
        {
            for (int i = 0; i < warpers.Length; i++)
            {
                warpers[i].SetActive(true);
                if (warpers[i].GetComponent<WarperBehaviour>().levelToWarp == maxLevelUnblocked)
                {
                    if (i+1 < warpers.Length)
                    {
                        warpers[i + 1].SetActive(true);
                    }
                    return;
                }
            }
        }
        Debug.Log("maxLevelUnblocked = " + maxLevelUnblocked);

    }

    // Update is called once per frame
    public void Reset()
    {
        _selectedLevel = "";
        warpButton.interactable = false;
        levelInfo.SetActive(false);
        logo.SetActive(true);
    }

    public void SelectLevel(string _value)
    {
        _selectedLevel = _value;
        warpButton.interactable = true;
        logo.SetActive(false);
        levelInfo.SetActive(true);
    }

    public void Warp()
    {
        Application.LoadLevel(_selectedLevel);
    }
}
