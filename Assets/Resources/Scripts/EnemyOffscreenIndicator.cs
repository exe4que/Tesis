using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyOffscreenIndicator : MonoBehaviour {

    public Transform goToTrack;
    private Image renderer;
    private Camera mainCamera;

    void Awake()
    {
        renderer = this.GetComponent<Image>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 v3Screen = mainCamera.WorldToViewportPoint(goToTrack.position);
        if (v3Screen.x > -0.01f && v3Screen.x < 1.01f && v3Screen.y > -0.01f && v3Screen.y < 1.01f)
            renderer.enabled = false;
        else
        {
            renderer.enabled = true;
            v3Screen.x = Mathf.Clamp(v3Screen.x, 0.01f, 0.99f);
            v3Screen.y = Mathf.Clamp(v3Screen.y, 0.01f, 0.99f);
            transform.position = Camera.main.ViewportToWorldPoint(v3Screen);
        }
        this.gameObject.SetActive(goToTrack.gameObject.activeSelf);

    }
}
