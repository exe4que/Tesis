using UnityEngine;
using System.Collections;
using System;

public class TileBehaviour : MonoBehaviour, Idamageable {

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

    public void Piew(int _bulletType)
    {
        if (_bulletType >= index)
        {
            MapManager.instance.OnTileDied(this.transform.position, nearbyTiles);
            PoolMaster.Spawn("Particles", "dirtExplosionEffect", this.transform.position);
            AudioManager.instance.PlaySound("DirtExplosion");
            this.gameObject.SetActive(false);
        }
    }
}
