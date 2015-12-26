using UnityEngine;
using System.Collections;

public class TileBehaviour : MonoBehaviour {

    public int index;
    public bool[] nearbyTiles = new bool[8] { false, false, false, false, false, false, false, false };

    void Awake()
    {
        //this.GetComponent<SpriteRenderer>().sprite = MapManager.instance.AssignSpriteToTileType
    }
    void Start()
    {
    }
}
