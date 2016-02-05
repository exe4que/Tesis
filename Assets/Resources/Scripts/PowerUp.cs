using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
    public int weaponID = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponentInChildren<Shoot>().PickUpWeapon(weaponID);
        this.gameObject.SetActive(false);
    }
}
