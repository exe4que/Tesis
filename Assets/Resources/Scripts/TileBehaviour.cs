using UnityEngine;
using System.Collections;
using System;

public class TileBehaviour : MonoBehaviour {

    public int index;
    public bool[] nearbyTiles = new bool[8] { false, false, false, false, false, false, false, false };

    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = MapManager.instance.AssignSpriteToTileType(nearbyTiles, index);
    }

    public void AddNearbyTile(int _value)
    {
        nearbyTiles[_value] = true;
    }

    public void DeleteNearbyTile(int _value)
    {
        nearbyTiles[_value] = false;
    }

    public void Piew()
    {
        if (Array.IndexOf(MapManager.destroyableTileIndexes, index) != -1)
        {
            MapManager.instance.OnTileDied(this.transform.position, nearbyTiles);
            this.gameObject.SetActive(false);
        }
    }
}
