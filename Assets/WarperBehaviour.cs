using UnityEngine;
using System.Collections;


public class WarperBehaviour : MonoBehaviour {

    public int levelToWarp = 0;

    void Awake()
    {
        this.GetComponentInChildren<TextMesh>().text = levelToWarp.ToString();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        LevelWarpManager.instance.SelectLevel(levelToWarp);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        LevelWarpManager.instance.Reset();
    }
}
