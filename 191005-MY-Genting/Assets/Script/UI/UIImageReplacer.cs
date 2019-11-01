using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageReplacer : MonoBehaviour
{
    public RectTransform[] newTransform;
    public RectTransform[] destinationTransform;

    [ContextMenu("Replace")]
    public void Replace()
    {
        for (int i = 0; i < newTransform.Length; i++)
        {
            newTransform[i].position = destinationTransform[i].position;
            newTransform[i].sizeDelta = destinationTransform[i].sizeDelta;
            newTransform[i].name = "TMP" + destinationTransform[i].name;
          //  Image label = destinationTransform[i].GetChild(2).GetComponent<Image>();
          //  newTransform[i].GetChild(2).GetComponent<Image>().sprite = label.sprite;
          //  newTransform[i].GetChild(2).GetComponent<Image>().SetNativeSize();
        }
    }

}
