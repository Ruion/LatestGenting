using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public float sec = 2f;
    public bool finish = false;

    private void OnEnable()
    {
        finish = false;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (!finish) yield return null;

        yield return new WaitForSecondsRealtime(sec);

        gameObject.SetActive(false);
    }

    public void Finish()
    {
        finish = true;
    }

}
