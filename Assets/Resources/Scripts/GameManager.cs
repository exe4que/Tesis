using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static float[] bulletDamageTable = new float[] { 5f, 10f, 15f };

    public static GameManager _instance;

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
