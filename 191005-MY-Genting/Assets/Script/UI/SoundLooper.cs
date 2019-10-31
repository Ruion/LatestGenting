using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLooper : MonoBehaviour
{
    public float delay1;
    public float delay2;
    public float interval = 5;
    public AudioSource audio1;
    public AudioSource audio2;
    public void StartLoop() {

        StartCoroutine(LoopRoutine());
    }

    public IEnumerator LoopRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay1);
            audio1.Play();

            yield return new WaitForSeconds(delay2);
            audio2.Play();

            yield return new WaitForSeconds(interval);

        }
    }
}
