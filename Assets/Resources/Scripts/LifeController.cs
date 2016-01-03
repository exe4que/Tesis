using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour, Idamageable {

    float life;
	public float lifeScalar = 100f;
    [Range(0, 1)]
    public float realLife = 1f;

    void Awake()
    {
        life = lifeScalar;
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
	}

    public void Piew(int _bulletType)
    {
        this.takeDamage(GameManager.bulletDamageTable[_bulletType]);
    }
}
