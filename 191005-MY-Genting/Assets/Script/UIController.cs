using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;
using DataBank;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class UIController : MonoBehaviour
{
    [Header("Game Script")]
    public UserDatabase User_Script;
    public VoucherDatabase Voucher_Script;
    public VoucherDistributionDatabase VoucherDistribution_Script;
    public DupReferDatabase DoubleUpRefer_Script;
    public RandomFeature Random_Script;
    public PrintController Print_Script;

    [Header("Variable")]
    public GameObject Spin_Object;
    public Sprite[] Voucher_Logo_Sprite;
    public Image[] voucher_spin_prize;
    public Text[] Registration_Input_Field;
    public TMP_Dropdown Registration_Phone_Country;
    public Button Registration_Button;
    public Text Registration_Input_Field_Inherit;
    public GameObject[] Registration_Warning_Input;
    public Image Loading_Card;
    public Text[] Card_Info_Input_Field;
    public InputField[] Card_Info_Input_Field_Parent;
    public Button Card_Info_Redeem_Button;
    public Button Card_Info_DU_Button;
    public TMP_Dropdown Card_Info_Dropdown_Phone;
    public GameObject[] Card_Info_Warning_Input;
    public Button DU_Button;
    public Text[] DU_Input_Field;
    public TMP_Dropdown DU_Phone_Country;
    public GameObject[] DU_Warning_Input;
    public Image[] DU_voucher_spin_prize;
    public Image DU_voucher_spin_prize_second;
    public GameObject DU_Spin_Object;
    public AudioSource spinSound;
    

    [Header("Pages")]
    public GameObject HomePage;
    public GameObject ProcessPage;
    public GameObject SpinPage;
    public GameObject SpinPrizePage;
    public GameObject VoucherPage;
    public GameObject RegistrationPage;
    public GameObject LoadingCardPage;
    public GameObject RedeemCardPage;
    public GameObject CardInfoPage;
    public GameObject DoubleUpPage;
    public GameObject SpinPage_DU;
    public GameObject SpinPrizePage_DU;
    public GameObject VoucherPage_DU;
    public GameObject PrintVoucherPage;


    bool registration_input1 = false;
    bool registration_input2 = false;
    bool registration_input3 = false;
    string MailPattern = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";
    string user_name = "0";
    string email = "0";
    string phone = "0";
    string status = "0";
    string memberid = "0";
    string refercode = "0";

    bool du_input1 = false;
    bool du_input2 = false;
    bool du_input3 = false;
    string du_name = "0";
    string du_email = "0";
    string du_phone = "0";

    string printV1 = "";
    string printV2 = "";
    string tempvouchername;

    bool exist = false;
    bool ci_input1 = false;
    bool ci_input2 = false;

    bool doubleupBack = false;

    // Start is called before the first frame update
    void Start()
    {
        Registration_Input_Field[1].horizontalOverflow = HorizontalWrapMode.Wrap;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 abc = Spin_Object.transform.position;
        Debug.Log(abc);*/
    }

    #region Spin Page Button
    public void SpinButton()
    {
        Random_Script.MakeRandomProbability();
        Random_Script.CalculateProbability();
        StartCoroutine(SpinWheel());
        foreach(Image a in voucher_spin_prize)
        {
            a.sprite = Voucher_Logo_Sprite[Voucher_Script.ChosenVoucher-1];
        }
        DU_voucher_spin_prize_second.sprite = Voucher_Logo_Sprite[Voucher_Script.ChosenVoucher - 1];
    }

    IEnumerator SpinWheel()
    {
        int counter = 18;
        float TimeWait = 0.01f;
        int countmulti = 0;
        if (Voucher_Script.ChosenVoucher == 1)
        {
            countmulti = 4;
        }
        else if(Voucher_Script.ChosenVoucher == 2)
        {
            countmulti = 14;
        }
        else if (Voucher_Script.ChosenVoucher == 3)
        {
            countmulti = 8;
        }
        else if (Voucher_Script.ChosenVoucher == 4)
        {
            countmulti = 17;
        }
        else if (Voucher_Script.ChosenVoucher == 5)
        {
            countmulti = 11;
        }
        else if (Voucher_Script.ChosenVoucher == 6)
        {
            countmulti = 20;
        }
        counter *= 5;
        counter += countmulti;
        int slowdown = counter - 18;
        Debug.Log(counter);
        for(int i = 0; i < counter; i++)
        {
            if(Spin_Object.transform.position.y >= 2697.2f)
            {
                Spin_Object.transform.position = new Vector2(Spin_Object.transform.position.x, -938.8f);
            }

            Spin_Object.transform.position = new Vector2(Spin_Object.transform.position.x, Spin_Object.transform.position.y + 202);
            if (i > slowdown)
            {
                TimeWait += 0.01f;
            }
            yield return new WaitForSeconds(TimeWait);
        }

        spinSound.Stop();
        yield return new WaitForSeconds(2);
        SpinPage.SetActive(false);
        SpinPrizePage.SetActive(true);
    }
    #endregion

    #region Spin Prize Page Button
    public void SpinPrizeButton()
    {
        SpinPrizePage.SetActive(false);
        VoucherPage.SetActive(true);
    }
    #endregion

    #region Voucher Page Button
    public void VoucherButton1_NewMember()
    {
        VoucherPage.SetActive(false);
        RegistrationPage.SetActive(true);
        exist = false;
        InvokeRepeating("R_setinput2", 1, 1);
    }
    public void VoucherButton2_ExistingMember()
    {
        status = "0";
        exist = true;
        VoucherPage.SetActive(false);
        CardInfoPage.SetActive(true);
    }
    #endregion

    #region Registration Page Button
    public void RegistrationButton()
    {
        user_name = Registration_Input_Field[0].text;
        email = Registration_Input_Field[1].text;
        phone = "+" + GetNumbers(Registration_Phone_Country.options[Registration_Phone_Country.value].text) + Registration_Input_Field[2].text;
        status = "1";
        refercode = Registration_Input_Field[3].text;
        if (refercode == "")
        {
            refercode = "0";
        }
        Debug.Log(user_name + "\t" + email + "\t" + phone + "\t" + refercode);
        CancelInvoke("R_setinput2");
        RegistrationPage.SetActive(false);
        //LoadingCardPage.SetActive(true);
        RedeemCardPage.SetActive(true);
        startLoading();
    }
    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }
    public void R_setinput1()
    {
        registration_input1 = true;
        Registration_Warning_Input[0].SetActive(true);
        Registration_Warning_Input[1].SetActive(false);
        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
    }
    public void R_setinput2()
    {
        registration_input2 = Regex.IsMatch(Registration_Input_Field[1].text, MailPattern);
        if(registration_input2)
        {
            Registration_Warning_Input[2].SetActive(true);
            Registration_Warning_Input[3].SetActive(false);
        }
        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
    }
    public void R_setinput3()
    {
        registration_input3 = true;
        Registration_Warning_Input[4].SetActive(true);
        Registration_Warning_Input[5].SetActive(false);
        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
    }

    public void BackBtnRegister()
    {
        CancelInvoke("R_setinput2");
        RegistrationPage.SetActive(false);
        VoucherPage.SetActive(true);
    }
    #endregion

    #region Loading Card Page
    public void startLoading()
    {
        StartCoroutine(LoadingisStart());
    }
    IEnumerator LoadingisStart()
    {
        yield return new WaitForSeconds(1);
        for(float i = 0f; i < 1; i+=0.2f)
        {
            Loading_Card.fillAmount += 0.2f;
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(1);
        LoadingCardPage.SetActive(false);
        RedeemCardPage.SetActive(true);
    }
    #endregion

    #region Redeem Card Page Button
    public void RedeemCardButton()
    {
        Card_Info_Input_Field_Parent[0].text = Registration_Input_Field_Inherit.text;
        Card_Info_Input_Field_Parent[1].text = refercode;
        if(refercode == "0")
        {
            Card_Info_Input_Field_Parent[1].text = "";
        }
        RedeemCardPage.SetActive(false);
        CardInfoPage.SetActive(true);
    }
    #endregion

    #region Card Info Page Button
    public void CardInfoButton1_Redeem()
    {
        if(doubleupBack)
        {
            CardInfoPage.SetActive(false);
            PrintVoucherPage.SetActive(true);
            PrintVoucherPageFunction();
        }
        else
        {
            if (exist)
            {
                phone = "+" + GetNumbers(Card_Info_Dropdown_Phone.options[Card_Info_Dropdown_Phone.value].text) + Card_Info_Input_Field[0].text;
                email = phone;
                refercode = Card_Info_Input_Field[1].text;
                if (refercode == "")
                {
                    refercode = "0";
                }
            }
            memberid = Card_Info_Input_Field[2].text;
            User_Script.InsertData(user_name, phone, email, status, memberid, refercode);
            CardInfoPage.SetActive(false);
            PrintVoucherPage.SetActive(true);
            printV1 = Voucher_Script.ChosenVoucher.ToString();
            tempvouchername = printV1;
            Voucher_Script.ClearList();
            Voucher_Script.GetData();
            foreach (VoucherEntity s in Voucher_Script.myList)
            {
                if (Voucher_Script.ChosenVoucher == s._id)
                {
                    Voucher_Script.MinusUpdateData(s);
                    VoucherDistribution_Script.InsertData(phone, Voucher_Script.ChosenVoucher);
                    break;
                }
            }
            PrintVoucherPageFunction();
        }
        
    }
    public void CardInfoButton2_DoubleUp()
    {
        if (doubleupBack)
        {
            CardInfoPage.SetActive(false);
            PrintVoucherPage.SetActive(true);
        }
        else
        {
            printV1 = Voucher_Script.ChosenVoucher.ToString();
            tempvouchername = printV1;
            Voucher_Script.ClearList();
            Voucher_Script.GetData();
            foreach (VoucherEntity s in Voucher_Script.myList)
            {
                if (Voucher_Script.ChosenVoucher == s._id)
                {
                    Voucher_Script.MinusUpdateData(s);
                    VoucherDistribution_Script.InsertData(phone, Voucher_Script.ChosenVoucher);
                    break;
                }
            }
            memberid = Card_Info_Input_Field[2].text;
            User_Script.InsertData(user_name, phone, email, status, memberid, refercode);
            InvokeRepeating("DU_setInput2", 1, 1);
            CardInfoPage.SetActive(false);
            DoubleUpPage.SetActive(true);
        }
        
    }
    public void CI_input1()
    {
        ci_input1 = true;
        Card_Info_Warning_Input[0].SetActive(true);
        Card_Info_Warning_Input[1].SetActive(false);
        if (exist)
        {
            if(ci_input1 && ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
        }
        else
        {
            if(ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
        }
    }
    public void CI_input2()
    {
        ci_input2 = true;
        Card_Info_Warning_Input[2].SetActive(true);
        Card_Info_Warning_Input[3].SetActive(false);
        if (exist)
        {
            if (ci_input1 && ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
        }
        else
        {
            if (ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
        }
    }

    public void BackBtnCardInfo()
    {
        CardInfoPage.SetActive(false);
        VoucherPage.SetActive(true);
    }
    #endregion

    #region Double Up Page Button
    public void DUButton()
    {
        du_name = DU_Input_Field[0].text;
        du_email = DU_Input_Field[1].text;
        du_phone = "+" + DU_GetNumbers(DU_Phone_Country.options[DU_Phone_Country.value].text) + DU_Input_Field[2].text;
        DoubleUpRefer_Script.InsertData(phone, du_name, du_phone, du_email);
        CancelInvoke("DU_setInput2");
        DoubleUpPage.SetActive(false);
        SpinPage_DU.SetActive(true);
    }
    private static string DU_GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }
    public void DU_setInput1()
    {
        du_input1 = true;
        DU_Warning_Input[0].SetActive(true);
        DU_Warning_Input[1].SetActive(false);
        if(du_input1 && du_input2 && du_input3)
        {
            DU_Button.interactable = true;
        }
    }
    public void DU_setInput2()
    {
        du_input2 = Regex.IsMatch(DU_Input_Field[1].text, MailPattern);
        if(du_input2)
        {
            DU_Warning_Input[2].SetActive(true);
            DU_Warning_Input[3].SetActive(false);
        }
        if (du_input1 && du_input2 && du_input3)
        {
            DU_Button.interactable = true;
        }
    }
    public void DU_setInput3()
    {
        du_input3 = true;
        DU_Warning_Input[4].SetActive(true);
        DU_Warning_Input[5].SetActive(false);
        if (du_input1 && du_input2 && du_input3)
        {
            DU_Button.interactable = true;
        }
    }

    public void BackBtnDoubleUp()
    {
        doubleupBack = true;
        DoubleUpPage.SetActive(false);
        CardInfoPage.SetActive(true);
    }
    #endregion

    #region Double Up Spin Page Button
    public void DU_SpinButton()
    {
        Voucher_Script.ClearList();
        Voucher_Script.GetData();
        foreach (VoucherEntity s in Voucher_Script.myList)
        {
            if (Voucher_Script.ChosenVoucher == s._id)
            {
                Voucher_Script.MinusUpdateData(s);
                break;
            }
        }
        Random_Script.MakeRandomProbability();
        Random_Script.CalculateProbability();
        StartCoroutine(DU_SpinWheel());
        foreach (Image a in DU_voucher_spin_prize)
        {
            a.sprite = Voucher_Logo_Sprite[Voucher_Script.ChosenVoucher - 1];
        }
    }

    IEnumerator DU_SpinWheel()
    {
        int counter = 18;
        float TimeWait = 0.01f;
        int countmulti = 0;
        if (Voucher_Script.ChosenVoucher == 1)
        {
            countmulti = 4;
        }
        else if (Voucher_Script.ChosenVoucher == 2)
        {
            countmulti = 14;
        }
        else if (Voucher_Script.ChosenVoucher == 3)
        {
            countmulti = 8;
        }
        else if (Voucher_Script.ChosenVoucher == 4)
        {
            countmulti = 17;
        }
        else if (Voucher_Script.ChosenVoucher == 5)
        {
            countmulti = 11;
        }
        else if (Voucher_Script.ChosenVoucher == 6)
        {
            countmulti = 20;
        }
        counter *= 5;
        counter += countmulti;
        int slowdown = counter - 18;
        Debug.Log(counter);
        for (int i = 0; i < counter; i++)
        {
            if (DU_Spin_Object.transform.position.y >= 2697.2f)
            {
                DU_Spin_Object.transform.position = new Vector2(DU_Spin_Object.transform.position.x, -938.8f);
            }

            DU_Spin_Object.transform.position = new Vector2(DU_Spin_Object.transform.position.x, DU_Spin_Object.transform.position.y + 202);
            if (i > slowdown)
            {
                TimeWait += 0.01f;
            }
            yield return new WaitForSeconds(TimeWait);
        }

        spinSound.Stop();
        yield return new WaitForSeconds(2);
        SpinPage_DU.SetActive(false);
        SpinPrizePage_DU.SetActive(true);
    }
    #endregion

    #region Spin Prize Page Button
    public void DU_SpinPrizeButton()
    {
        SpinPrizePage_DU.SetActive(false);
        VoucherPage_DU.SetActive(true);
    }
    #endregion

    #region Voucher Page Button
    public void DU_VoucherButton()
    {
        Voucher_Script.ClearList();
        Voucher_Script.GetData();
        foreach (VoucherEntity s in Voucher_Script.myList)
        {
            if (Voucher_Script.ChosenVoucher == s._id)
            {
                Voucher_Script.MinusUpdateData(s);
                VoucherDistribution_Script.InsertData(phone, Voucher_Script.ChosenVoucher);
                break;
            }
        }
        VoucherPage_DU.SetActive(false);
        PrintVoucherPage.SetActive(true);
        printV2 = Voucher_Script.ChosenVoucher.ToString();
        tempvouchername += "-" + printV2;
        PrintVoucherPageFunction();
    }
    #endregion

    #region Print Voucher Page
    public void PrintVoucherPageFunction()
    {
        Debug.LogError("File name = " + tempvouchername + ".pdf");
        Print_Script.Print(tempvouchername);
        //StartCoroutine(WaitRestartscene());
    }
    /*IEnumerator WaitRestartscene()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("Main");
    }*/
    public void OnCompleteAnimationCard()
    {
        SceneManager.LoadScene("Main");
    }
    #endregion
}
