using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
    public int weaponID = 0;

    void OnEnable()
    {
        Invoke("Disable", 10f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponentInChildren<Shoot>().PickUpWeapon(weaponID);
        AudioManager.instance.PlaySound("PowerUp");
        this.gameObject.SetActive(false);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
