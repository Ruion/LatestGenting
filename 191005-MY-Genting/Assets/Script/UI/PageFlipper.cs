using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFlipper : MonoBehaviour
{
    public Animator prevAnimator;
    public GameObject nextPage;

    public void FlipNextPage()
    {
        prevAnimator.Play("pageCloseClip");
        nextPage.SetActive(true);
    }
}
