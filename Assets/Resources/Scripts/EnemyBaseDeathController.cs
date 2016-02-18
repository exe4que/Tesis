using UnityEngine;
using System.Collections;

public class EnemyBaseDeathController : MonoBehaviour, IDestroyable {

    bool enabled = true;
    public void OnDeath()
    {
        if (enabled)
        {
            enabled = false;
            GameManager.instance.DeactivateBaseIndicator(this.gameObject.GetInstanceID());
            AudioManager.instance.PlaySound("BaseDown");
            EnemySpawnManager.instance.OnBaseDestroyed();
        }
        this.gameObject.SetActive(false);
    }
}
