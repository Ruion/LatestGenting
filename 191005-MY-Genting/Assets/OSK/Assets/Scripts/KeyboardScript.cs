using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{

    public AudioSource clickSound;

    private void OnEnable()
    {
        if (Application.platform == RuntimePlatform.Android)
            gameObject.SetActive(false);
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameObject.SetActive(false);
    }

    public TMPro.TMP_InputField inputFieldTMPro_
    {
        get { return inputFieldTMPro; }
        set
        {
            inputFieldTMPro = value;
        }
    }

    public TMPro.TMP_InputField inputFieldTMPro;
    public GameObject EngLayoutSml, EngLayoutBig, SymbLayout;



    public void alphabetFunction(string alphabet)
    {
        clickSound.Play();
        bool canType = true;

        if (inputFieldTMPro.contentType == TMPro.TMP_InputField.ContentType.IntegerNumber)
        {
            int out_;
            if (!int.TryParse(alphabet, out out_)) canType = false;
          // SAFE if(int.TryParse(alphabet, out out_)) inputFieldTMPro.text += alphabet;
            
        }

        if (canType) {
            inputFieldTMPro.text = inputFieldTMPro.text.Insert(inputFieldTMPro.stringPosition, alphabet);
            inputFieldTMPro.stringPosition += alphabet.Length;
            inputFieldTMPro.Select();
        }

        // SAFE  else inputFieldTMPro.text += alphabet; 
    }

    public void BackSpace()
    {
        clickSound.Play();
        // SAFE if (inputFieldTMPro.text.Length>0) inputFieldTMPro.text= inputFieldTMPro.text.Remove(inputFieldTMPro.text.Length-1);

        if (inputFieldTMPro.text.Length < 0) return;

        if ( inputFieldTMPro.stringPosition - 1 < 0) return;

        int cutPos = inputFieldTMPro.stringPosition - 1;

        inputFieldTMPro.text = inputFieldTMPro.text.Remove(cutPos, 1);
        inputFieldTMPro.stringPosition = cutPos;
        inputFieldTMPro.Select();
        //  if (inputFieldTMPro.stringPosition <= 0) inputFieldTMPro.MoveTextEnd(false);

    }

    public void CloseAllLayouts()
    {
        EngLayoutSml.SetActive(false);
        EngLayoutBig.SetActive(false);
        SymbLayout.SetActive(false);

    }

    public void ShowLayout(GameObject SetLayout)
    {
        clickSound.Play();
        CloseAllLayouts();
        SetLayout.SetActive(true);

    }

}
