using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public InputField inputField
    {
        get { return inputTextField; }
        set
        {
            inputTextField = value;
        }
    }

    public InputField inputTextField;
    public GameObject EngLayoutSml, EngLayoutBig, SymbLayout;



    public void alphabetFunction(string alphabet)
    {
        clickSound.Play();
        inputTextField.text=inputTextField.text + alphabet;

    }

    public void BackSpace()
    {
        clickSound.Play();
        if (inputTextField.text.Length>0) inputTextField.text= inputTextField.text.Remove(inputTextField.text.Length-1);
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
