using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	public bool isBot, fireButton;
    public GameObject activeWeapon;


    void Start()
    {
        AssignWeapon(0);
    }

    private void AssignWeapon(int _weaponID)
    {
        activeWeapon = Instantiate(GameManager.instance.weaponsList[_weaponID], this.transform.position, this.transform.rotation) as GameObject;
        activeWeapon.transform.SetParent(this.transform);
    }

	void Update ()
	{
        if (!isBot)
        {
            fireButton = InputManager.instance.GetButton("Fire1");
        }
		if (fireButton) {
            if (isBot)
            {
                OnShoot(GameManager.instance.botMask);
            }
            else
            {
                OnShoot(GameManager.instance.playerMask);
            }
		}
	}

	void OnShoot (LayerMask _validTarget)
	{
        activeWeapon.GetComponent<IFireable>().OnShoot(_validTarget, isBot);
	}

    public void PickUpWeapon(int _index)
    {
        Destroy(this.transform.GetChild(0).gameObject);
        AssignWeapon(_index);
        Invoke("RestoreWeapon", 10f);
    }

    private void RestoreWeapon()
    {
        AssignWeapon(0);
    }
}
