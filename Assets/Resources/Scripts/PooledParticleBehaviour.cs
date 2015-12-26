using UnityEngine;
using System.Collections;

public class PooledParticleBehaviour : MonoBehaviour {

	void OnEnable()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
