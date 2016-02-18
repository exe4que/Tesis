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

        Invoke("ShowStars", 1f);
        isEnabled = true;
    }

    
    void Update()
    {
        if (isEnabled)
        {
            initLife = (initLife < targetLife * 100 - 0.1f)? Mathf.Lerp(initLife, targetLife * 100f, Time.smoothDeltaTime * 3f) : targetLife * 100;
            initTime = (initTime < targetTime - 0.1f)? Mathf.Lerp(initTime, targetTime, Time.smoothDeltaTime * 3f) : targetTime;
            life.text = "Life: " + initLife.ToString("F2") + "%";
            timeLeft.text = "Time Left: " + initTime.ToString("F2") + "s";
        }
    }

    private void ShowStars()
    {
        float _value = targetLife * targetTime;
        Debug.Log("_value = " + _value);
        int res = 0;

        if (_value >= 60) res = 3;
        if (_value >= 40 && _value < 60) res = 2;
        if (_value < 40) res = 1;

        starsPanel.ShowStars(res);
    }
        
}
