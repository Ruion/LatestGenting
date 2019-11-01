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

        int caretPos = inputFieldTMPro.caretPosition;
        Debug.Log("caret position before type: " + inputFieldTMPro.caretPosition);

        if (inputFieldTMPro.contentType == TMPro.TMP_InputField.ContentType.IntegerNumber)
        {
            int out_;
            if(int.TryParse(alphabet, out out_)) inputFieldTMPro.text = inputFieldTMPro.text.Insert(inputFieldTMPro.caretPosition, alphabet); //inputFieldTMPro.text = inputFieldTMPro.text + alphabet;
                                                                                                                                              //  if(int.TryParse(alphabet, out out_)) inputFieldTMPro.text += alphabet;
            
            }
        else inputFieldTMPro.text = inputFieldTMPro.text.Insert(inputFieldTMPro.caretPosition, alphabet); //inputFieldTMPro.text = inputFieldTMPro.text + alphabet;
                                                                                                          //   else inputFieldTMPro.text += alphabet;
        Debug.Log("caret position after type: " + inputFieldTMPro.caretPosition);
        inputFieldTMPro.caretPosition++;
        //   if (caretPos == 0) { inputFieldTMPro.caretPosition--; inputFieldTMPro.ActivateInputField(); }
        //  inputFieldTMPro.caretPosition = inputFieldTMPro.stringPosition;
    }

    public void BackSpace()
    {
        clickSound.Play();
        if (inputFieldTMPro.text.Length>0) inputFieldTMPro.text= inputFieldTMPro.text.Remove(inputFieldTMPro.text.Length-1);
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
