using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	public bool isBot, fireButton;
    public GameObject activeWeapon;
    private float timeToResetWeapon = -99f;


    void Start()
    {
        AssignWeapon(0);
    }

    private void AssignWeapon(int _weaponID)
    {
        activeWeapon = Instantiate(GameManager.instance.weaponsList[_weaponID], this.transform.position, this.transform.rotation) as GameObject;
        activeWeapon.transform.SetParent(this.transform);
    }

	public void ShootNow()
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
        CheckResetWeapon();
	}

    private void CheckResetWeapon()
    {
        if (timeToResetWeapon != -99f)
        {
            if (timeToResetWeapon <= 0f)
            {
                ResetWeapon();
                timeToResetWeapon = -99f;
            }
            else
            {
                timeToResetWeapon -= Time.deltaTime;
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
        timeToResetWeapon = 10f;
    }

    private void ResetWeapon()
    {
        AssignWeapon(0);
    }
}
