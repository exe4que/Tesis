using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static float[] bulletDamageTable = new float[] { 5f, 10f, 15f };

    private GameObject[] enemyBases;
    private Image[] enemyBasesIndicators;
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
        _instance = this;
    }

    void Start()
    {
        InitIndicators();
    }

    public void DeactivateBaseIndicator(int _instanceId)
    {
        Color redColor = new Color(1f, 0.392f, 0.392f);
        for (int i = 0; i < enemyBases.Length; i++)
        {
            if (enemyBases[i].GetInstanceID() == _instanceId)
            {
                enemyBasesIndicators[i].GetComponent<Image>().color = redColor;
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

}
