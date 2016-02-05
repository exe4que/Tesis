using UnityEngine;
using System.Collections;

public class BulletFormBehaviour : MonoBehaviour, IBullet {
    public GameObject[] children;
    private int inactiveChildren = 0;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            child.SetParent(null);
        }
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetValidTarget(LayerMask _validTarget)
    {
        BulletBehaviour[] children = this.transform.GetComponentsInChildren<BulletBehaviour>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetValidTarget(_validTarget);
        }
    }

    public void OnChildDeath()
    {
        inactiveChildren++;
        if (inactiveChildren == this.transform.childCount)
        {
            this.gameObject.SetActive(false);
        }
    }
}
