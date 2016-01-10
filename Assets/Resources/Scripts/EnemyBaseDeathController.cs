using UnityEngine;
using System.Collections;

public class EnemyBaseDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        GameManager.instance.DeactivateBaseIndicator(this.gameObject.GetInstanceID());
        this.gameObject.SetActive(false);
    }
}
