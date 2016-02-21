using UnityEngine;
using System.Collections;


public class WarperBehaviour : MonoBehaviour {

    public string levelToWarp;

    void Awake()
    {
        this.GetComponentInChildren<TextMesh>().text = levelToWarp.ToString();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        LevelWarpManager.instance.SelectLevel(levelToWarp);
        AudioManager.instance.PlaySound("Select");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        LevelWarpManager.instance.Reset();
    }
}
