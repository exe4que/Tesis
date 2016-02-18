using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButtonBehaviour : MonoBehaviour {

    private Image muteSprite;
    private bool enabled = false;

    void Awake()
    {
        muteSprite = this.transform.GetChild(0).GetComponent<Image>();
        muteSprite.enabled = (PlayerPrefs.GetInt("Music") == 0);
        enabled = !muteSprite.enabled;
    }

    public void OnClick()
    {
        enabled = !enabled;
        muteSprite.enabled = !enabled;
        int aux = enabled ? 1 : 0;
        PlayerPrefs.SetInt("Music", aux);
        AudioManager.instance.ToogleMusic();
    }
}
