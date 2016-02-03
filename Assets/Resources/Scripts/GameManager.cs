using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static float[] bulletDamageTable = new float[] { 5f, 10f, 15f };

    private GameObject[] enemyBases;
    private Image[] enemyBasesIndicators;
    private Text clockText;
    public float roundTime = 180f;
    public int activeBases = 6;
    

    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        this.enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        this.enemyBasesIndicators = GameObject.Find("MainCanvas/BasesPanel").GetComponentsInChildren<Image>();
        this.clockText = GameObject.Find("MainCanvas/TimeLeftText/ClockText").GetComponent<Text>();
        this.clockText.text = FormatTime(this.roundTime);
        EnemySpawnManager.instance.enemyBases = enemyBases.Length;
        _instance = this;
    }

    void Start()
    {
        InitIndicators();
    }

    void Update()
    {
        roundTime -= Time.deltaTime;
        this.clockText.text = roundTime >= 0 ? FormatTime(roundTime) : "00:00";
        this.enabled = roundTime > 0;
    }

    public void DeactivateBaseIndicator(int _instanceId)
    {
        for (int i = 0; i < enemyBases.Length; i++)
        {
            if (enemyBases[i].GetInstanceID() == _instanceId)
            {
                enemyBasesIndicators[i].GetComponent<IAnimable>().SetBool("TurningOff", true);
                activeBases--;
            }
        }
    }

    void InitIndicators()
    {
        for (int i = 0; i < enemyBasesIndicators.Length; i++)
        {
            if (i >= enemyBases.Length)
            {
                Image thisImage = enemyBasesIndicators[i].GetComponent<Image>();
                Color c = thisImage.color;
                c.a = 0.5f;
                thisImage.color = c;
                activeBases--;
            }
        }
    }

    string FormatTime(float _seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(_seconds);

        string ret = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);
        return ret;
    }

}
