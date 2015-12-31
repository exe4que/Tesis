using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {

	[Range (0, 100)]
	public float life = 100f;
	public float lifeScalar = 100f;
	[HideInInspector]
	public float realLife = 1f;

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
