using UnityEngine;
using System.Collections;

public class PlayerDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        this.gameObject.SetActive(false);
    }
}
