using UnityEngine;
using System.Collections;

public class FireParticleBehaviour : MonoBehaviour, IBullet
{
    public Gradient gradient;
    [Range(0f, 50f)]
    public float velocity = 15f;

    void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().color = gradient.Evaluate(Random.Range(0f,1f));
        Invoke("Disable", 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col != null)
            col.GetComponent<Idamageable>().Piew(1);
    }

    public void SetValidTarget(LayerMask _validTarget)
    {
        throw new System.NotImplementedException();
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
