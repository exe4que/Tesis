using UnityEngine;
using System.Collections;

public class EnemyBaseDeathController : MonoBehaviour, IDestroyable {

    public void OnDeath()
    {
        GameManager.instance.DeactivateBaseIndicator(this.gameObject.GetInstanceID());
        AudioManager.instance.PlaySound("BaseDown");
        EnemySpawnManager.instance.OnBaseDestroyed();
        this.gameObject.SetActive(false);
    }
}
