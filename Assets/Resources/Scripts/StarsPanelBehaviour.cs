using UnityEngine;
using System.Collections;

public class StarsPanelBehaviour : MonoBehaviour {

    private AnimatorInterface[] stars;

    void Awake()
    {
        stars = new AnimatorInterface[3];
        stars[0] = this.transform.FindChild("Star1").GetComponent<AnimatorInterface>();
        stars[1] = this.transform.FindChild("Star2").GetComponent<AnimatorInterface>();
        stars[2] = this.transform.FindChild("Star3").GetComponent<AnimatorInterface>();
    }

    public void ShowStars(int _amount)
    {
        StartCoroutine("Show", _amount);
    }

    IEnumerator Show(int _amount)
    {
        Debug.Log("_amount = " + _amount);
        for (int i = 0; i < _amount; i++)
        {
            stars[i].SetBool("Start", true);
            yield return new WaitForSeconds(1f);
        }
        Time.timeScale = 0;
    }

}
