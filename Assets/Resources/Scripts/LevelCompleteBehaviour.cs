using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCompleteBehaviour : MonoBehaviour
{
    private Text life, timeLeft;
    private StarsPanelBehaviour starsPanel;
    private LifeController playerLifeController;
    bool isEnabled = false;

    float initLife = 0f, targetLife = 0f, initTime = 0f, targetTime = 0f;

    void Awake()
    {
        life = this.transform.FindChild("Life").GetComponent<Text>();
        timeLeft = this.transform.FindChild("TimeLeft").GetComponent<Text>();
        starsPanel = this.transform.FindChild("StarsPanel").GetComponent<StarsPanelBehaviour>();
        playerLifeController = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeController>();
    }

    void OnEnable()
    {
        targetLife = playerLifeController.realLife;
        targetTime = GameManager.instance.roundTime;
        isEnabled = true;
    }


    void Update()
    {
        if (isEnabled)
        {

            initLife = (initLife < targetLife * 100 - 0.1f) ? Mathf.Lerp(initLife, targetLife * 100f, Time.smoothDeltaTime * 2f) : targetLife * 100;
            initTime = (initTime < targetTime - 0.1f) ? Mathf.Lerp(initTime, targetTime, Time.smoothDeltaTime * 2f) : targetTime;
            life.text = "Life: " + initLife.ToString("F2") + "%";
            timeLeft.text = "Time Left: " + initTime.ToString("F2") + "s";

            if (initLife != targetLife && initTime != targetTime)
            {
                AudioManager.instance.PlaySound("Score");
            }
            else
            {
                this.ShowStars();
                isEnabled = false;
            }
        }
    }

    private void ShowStars()
    {
        float _value = targetLife * targetTime;

        int res = DetermineStars(_value);

        int oldMaxLevel = int.Parse(PlayerPrefs.GetString("MaxLevelUnblocked").Replace("Level", ""));
        int thisLevel = int.Parse(Application.loadedLevelName.Replace("Level", ""));

        Debug.Log("oldMaxLevel = " + oldMaxLevel + ", thisLevel = " + thisLevel);
        if (oldMaxLevel < thisLevel)
        {
            PlayerPrefs.SetString("MaxLevelUnblocked", Application.loadedLevelName);
        }
        PlayerPrefs.SetInt(Application.loadedLevelName + " - Stars", res);
        starsPanel.ShowStars(res);
    }

    private static int DetermineStars(float _value)
    {
        if(_value <= 20)
            return 1;
        if (_value <= 40)
            return 2;
        if (_value > 40)
            return 3;
        return 0;
    }

}
