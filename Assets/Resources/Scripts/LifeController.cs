using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour, Idamageable {

    float life;
	public float lifeScalar = 100f;
    [Range(0, 1)]
    public float realLife = 1f;

    IAnimable animator;

    void OnEnable()
    {
        life = lifeScalar;
        realLife = 1;
        animator = this.GetComponent<IAnimable>();
    }

	public void takeDamage(float value){
		life -= life - value >= 0 ? value : 0f;
		UpdateRealLife ();
	}

	public void takeHeal(float value){
		life += life + value <= lifeScalar ? value : 0f;
		UpdateRealLife ();
	}

	void UpdateRealLife ()
	{
		realLife = life / lifeScalar;

        if (realLife == 0)
        {
            this.GetComponent<IDestroyable>().OnDeath();
        }
	}

    public void Piew(int _bulletType)
    {
        if (animator != null)
        {
            this.animator.SetBool("isFlashing", true);
        }
        this.takeDamage(GameManager.bulletDamageTable[_bulletType]);
    }
}
