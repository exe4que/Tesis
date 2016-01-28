using UnityEngine;
using System.Collections;

public class TileParticlesEffect : MonoBehaviour {

    public Sprite[] particles;
    public AnimationCurve alphaCurve;
    public float velocity;

    private SpriteRenderer renderer;
    private GameObject childSprite;
	// Use this for initialization
	void Awake () {
        childSprite = new GameObject("wave");
        renderer = childSprite.AddComponent<SpriteRenderer>();
        renderer.sortingOrder = 2;
        childSprite.transform.position = this.transform.position;
        childSprite.transform.SetParent(this.transform);
        renderer.sprite = particles[(int)Random.Range(0, particles.Length - 1)];
	}
	
	void Update () {
	}
}
