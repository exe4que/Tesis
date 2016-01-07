using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static float[] bulletDamageTable = new float[] { 5f, 10f, 15f };

    public int enemyBasesCant
    {
        get
        {
            return enemyBasesCant;
        }
        set
        {
            enemyBasesCant = value;
            activeEnemyBases = value;
        }
    }

    public int activeEnemyBases { get; set; }

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
        _instance = this;
    }

}
