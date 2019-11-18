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
    public CardDispense CardDispense_Script;

    [Header("Variable")]
    public GameObject Spin_Object;
    public Sprite[] Voucher_Logo_Sprite;
    public Image[] voucher_spin_prize;
    public TMP_InputField[] Registration_Input_Field;
    public TMP_Dropdown Registration_Phone_Country;
    public Button Registration_Button;
    public TMP_InputField Registration_Input_Field_Inherit;
    public GameObject[] Registration_Warning_Input;
    public GameObject Registration_Phone_Duplication_Warning;
    public Image Loading_Card;
    public Button Card_Info_BackButton;
    public TMP_InputField[] Card_Info_Input_Field;
    public TMP_InputField[] Card_Info_Input_Field_Parent;
    public Button Card_Info_Redeem_Button;
    public Button Card_Info_DU_Button;
    public TMP_Dropdown Card_Info_Dropdown_Phone;
    public GameObject[] Card_Info_Warning_Input;
    public GameObject Card_Info_Phone_Duplication_Warning;
    public GameObject Card_Info_CardMember_Duplication_Warning;
    public Button DU_Button;
    public TMP_InputField[] DU_Input_Field;
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
    bool cardinfoBack = false;
    bool getcard = false;

    bool Countdown = false;
    bool sec_Countdown = false;
    float initTime = 30f;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        time = initTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 abc = Spin_Object.transform.position;
        Debug.LogError(abc);*/
        if (Countdown)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene("Main");
            }
        }
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            time = initTime;
        }
    }

    public void Startcount()
    {
        Countdown = true;
    }


    #region Spin Page Button
    public void SpinButton()
    {
        time = initTime;
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
        int counter = 24;
        float TimeWait = 0.01f;
        int countmulti = 0;
        if (Voucher_Script.ChosenVoucher == 1)
        {
            countmulti = 3; //1
        }
        else if(Voucher_Script.ChosenVoucher == 2)
        {
            countmulti = 13; //4
        }
        else if (Voucher_Script.ChosenVoucher == 3)
        {
            countmulti = 7; //2
        }
        else if (Voucher_Script.ChosenVoucher == 4)
        {
            countmulti = 16; //5
        }
        else if (Voucher_Script.ChosenVoucher == 5)
        {
            countmulti = 10; //3
        }
        else if (Voucher_Script.ChosenVoucher == 6)
        {
            countmulti = 19; //6
        }
        else if (Voucher_Script.ChosenVoucher == 7)
        {
            countmulti = 22; //7
        }
        else if (Voucher_Script.ChosenVoucher == 8)
        {
            countmulti = 25; //8
        }
        counter *= 5;
        counter += countmulti;
        int slowdown = counter - 24;
        Debug.Log(counter);
        for(int i = 0; i < counter; i++)
        {
            if(Spin_Object.transform.position.y >= 3298.5f)
            {
                Spin_Object.transform.position = new Vector2(Spin_Object.transform.position.x, -1549.5f);
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
        time = initTime;
        SpinPrizePage.SetActive(false);
        VoucherPage.SetActive(true);
    }
    #endregion

    #region Voucher Page Button
    public void VoucherButton1_NewMember()
    {
        time = initTime;
        VoucherPage.SetActive(false);
        RegistrationPage.SetActive(true);
        exist = false;
        InvokeRepeating("R_setinput2", 1, 1);
    }
    public void VoucherButton2_ExistingMember()
    {
        time = initTime;
        status = "0";
        exist = true;
        VoucherPage.SetActive(false);
        CardInfoPage.SetActive(true);
    }
    #endregion

    #region Registration Page Button
    public void RegistrationButton()
    {
        time = initTime;
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
        
        if(getcard)
        {
            CardInfoPage.SetActive(true);
        }
        else
        {
            LoadingCardPage.SetActive(true);
            startLoading();
        }
        
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
        if (Registration_Input_Field[0].text == "")
        {
            registration_input1 = false;
            Registration_Warning_Input[0].SetActive(false);
            Registration_Warning_Input[1].SetActive(true);
        }

        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
        else
        {
            Registration_Button.interactable = false;
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
        else
        {
            Registration_Warning_Input[2].SetActive(false);
            Registration_Warning_Input[3].SetActive(true);
        }

        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
        else
        {
            Registration_Button.interactable = false;
        }
    }
    public void R_setinput3()
    {
        registration_input3 = true;
        Registration_Warning_Input[4].SetActive(true);
        Registration_Warning_Input[5].SetActive(false);
        Registration_Phone_Duplication_Warning.SetActive(false);

        string tempphone = "+" + GetNumbers(Registration_Phone_Country.options[Registration_Phone_Country.value].text) + Registration_Input_Field[2].text;
        foreach (UserEntity f in User_Script.myList)
        {
            if (tempphone == f._phone)
            {
                Registration_Phone_Duplication_Warning.SetActive(true);
                registration_input3 = false;
                Registration_Warning_Input[4].SetActive(false);
                Registration_Warning_Input[5].SetActive(true);
            }
        }
        if(Registration_Input_Field[2].text == "" || Registration_Input_Field[2].text.Length <= 6)
        {
            registration_input3 = false;
            Registration_Warning_Input[4].SetActive(false);
            Registration_Warning_Input[5].SetActive(true);
        }
        if (registration_input1 && registration_input2 && registration_input3)
        {
            Registration_Button.interactable = true;
        }
        else
        {
            Registration_Button.interactable = false;
        }
    }

    public void BackBtnRegister()
    {
        time = initTime;
        CancelInvoke("R_setinput2");
        RegistrationPage.SetActive(false);
        VoucherPage.SetActive(true);
    }

    public void Registration_CheckPhoneDuplicate()
    {
        Debug.Log("Checking Regsitration Phone Duplicate: Get All User Data Stage!!");
        User_Script.ClearList();
        User_Script.GetAllData();
    }
    #endregion

    #region Loading Card Page
    public void startLoading()
    {
        time = initTime;
        StartCoroutine(LoadingisStart());
    }
    IEnumerator LoadingisStart()
    {
        yield return new WaitForSeconds(1);
        for(float i = 0f; i < 1; i+=0.2f)
        {
            Loading_Card.fillAmount += 0.2f;
            yield return new WaitForSeconds(0.5f);
        }
        CardDispense_Script.DispenseCard();
        getcard = true;
        Card_Info_BackButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        LoadingCardPage.SetActive(false);
        RedeemCardPage.SetActive(true);
    }
    #endregion

    #region Redeem Card Page Button
    public void RedeemCardButton()
    {
        time = initTime;
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
        time = initTime;
        if (doubleupBack)
        {
            User_Script.InsertData(user_name, phone, email, status, memberid, refercode);
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

                    //increase given voucher data
                    string given = "given-" + s._type;
                    int temp_given = PlayerPrefs.GetInt(given, 0);
                    temp_given += 1;
                    PlayerPrefs.SetInt(given, temp_given);

                    //decrease voucher balance data
                    int defaultremain = 0;
                    if(s._id == 1)
                    {
                        defaultremain = 45;
                    }
                    else if(s._id == 2)
                    {
                        defaultremain = 12;
                    }
                    else if (s._id == 3)
                    {
                        defaultremain = 5;
                    }
                    else if (s._id == 4)
                    {
                        defaultremain = 50;
                    }
                    else if (s._id == 5)
                    {
                        defaultremain = 45;
                    }
                    else if (s._id == 6)
                    {
                        defaultremain = 45;
                    }
                    else if (s._id == 7)
                    {
                        defaultremain = 60;
                    }
                    else if (s._id == 8)
                    {
                        defaultremain = 200;
                    }
                    string remain = "remaining-" + s._type;
                    int temp_remain = PlayerPrefs.GetInt(remain, defaultremain);
                    temp_remain -= 1;
                    PlayerPrefs.SetInt(remain, temp_remain);
                    break;
                }
            }
            PrintVoucherPageFunction();
        }
        
    }
    public void CardInfoButton2_DoubleUp()
    {
        time = initTime;
        if (doubleupBack)
        {
            CardInfoPage.SetActive(false);
            DoubleUpPage.SetActive(true);
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

                    //increase given voucher data
                    string given = "given-" + s._type;
                    int temp_given = PlayerPrefs.GetInt(given, 0);
                    temp_given += 1;
                    PlayerPrefs.SetInt(given, temp_given);

                    //decrease voucher balance data
                    int defaultremain = 0;
                    if (s._id == 1)
                    {
                        defaultremain = 45;
                    }
                    else if (s._id == 2)
                    {
                        defaultremain = 12;
                    }
                    else if (s._id == 3)
                    {
                        defaultremain = 5;
                    }
                    else if (s._id == 4)
                    {
                        defaultremain = 50;
                    }
                    else if (s._id == 5)
                    {
                        defaultremain = 45;
                    }
                    else if (s._id == 6)
                    {
                        defaultremain = 45;
                    }
                    else if (s._id == 7)
                    {
                        defaultremain = 60;
                    }
                    else if (s._id == 8)
                    {
                        defaultremain = 200;
                    }
                    string remain = "remaining-" + s._type;
                    int temp_remain = PlayerPrefs.GetInt(remain, defaultremain);
                    temp_remain -= 1;
                    PlayerPrefs.SetInt(remain, temp_remain);
                    break;
                }
            }
            memberid = Card_Info_Input_Field[2].text;
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
        Card_Info_Phone_Duplication_Warning.SetActive(false);

        string tempphone = "+" + GetNumbers(Card_Info_Dropdown_Phone.options[Card_Info_Dropdown_Phone.value].text) + Card_Info_Input_Field[0].text;
        foreach(UserEntity gg in User_Script.myList)
        {
            if (tempphone == gg._phone)
            {
                Card_Info_Phone_Duplication_Warning.SetActive(true);
                ci_input1 = false;
                Card_Info_Warning_Input[0].SetActive(false);
                Card_Info_Warning_Input[1].SetActive(true);
            }
        }
        if(Card_Info_Input_Field[0].text == "" || Card_Info_Input_Field[0].text.Length <= 6)
        {
            ci_input1 = false;
            Card_Info_Warning_Input[0].SetActive(false);
            Card_Info_Warning_Input[1].SetActive(true);
        }

        if (exist)
        {
            if(ci_input1 && ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
            else
            {
                Card_Info_Redeem_Button.interactable = false;
                Card_Info_DU_Button.interactable = false;
            }
        }
        else
        {
            if(ci_input1 && ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
            else
            {
                Card_Info_Redeem_Button.interactable = false;
                Card_Info_DU_Button.interactable = false;
            }
        }
    }
    public void CI_input2()
    {
        ci_input2 = true;
        Card_Info_Warning_Input[2].SetActive(true);
        Card_Info_Warning_Input[3].SetActive(false);
        Card_Info_CardMember_Duplication_Warning.SetActive(false);

        string temp_card_id = Card_Info_Input_Field[2].text;
        foreach (UserEntity gg in User_Script.myList)
        {
            if (temp_card_id == gg._memberid)
            {
                Card_Info_CardMember_Duplication_Warning.SetActive(true);
                ci_input2 = false;
                Card_Info_Warning_Input[2].SetActive(false);
                Card_Info_Warning_Input[3].SetActive(true);
            }
        }

        if(Card_Info_Input_Field[2].text == "" || Card_Info_Input_Field[2].text.Length <= 7)
        {
            ci_input2 = false;
            Card_Info_Warning_Input[2].SetActive(false);
            Card_Info_Warning_Input[3].SetActive(true);
        }

        if (exist)
        {
            if (ci_input1 && ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
            else
            {
                Card_Info_Redeem_Button.interactable = false;
                Card_Info_DU_Button.interactable = false;
            }
        }
        else
        {
            if (ci_input2)
            {
                Card_Info_Redeem_Button.interactable = true;
                Card_Info_DU_Button.interactable = true;
            }
            else
            {
                Card_Info_Redeem_Button.interactable = false;
                Card_Info_DU_Button.interactable = false;
            }
        }
    }

    public void BackBtnCardInfo()
    {
        time = initTime;
        cardinfoBack = true;
        CardInfoPage.SetActive(false);
        VoucherPage.SetActive(true);
    }

    public void CardInfor_CheckPhoneDuplicate()
    {
        Debug.Log("Checking Card Info Phone Duplicate: Get All User Data Stage!!");
        User_Script.ClearList();
        User_Script.GetAllData();
    }
    #endregion

    #region Double Up Page Button
    public void DUButton()
    {
        time = initTime;
        User_Script.InsertData(user_name, phone, email, status, memberid, refercode);
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

        if(DU_Input_Field[0].text == "")
        {
            du_input1 = false;
            DU_Warning_Input[0].SetActive(false);
            DU_Warning_Input[1].SetActive(true);
        }

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
        else
        {
            DU_Warning_Input[2].SetActive(false);
            DU_Warning_Input[3].SetActive(true);
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

        if (DU_Input_Field[2].text == "" || DU_Input_Field[2].text.Length <= 6)
        {
            du_input3 = false;
            DU_Warning_Input[4].SetActive(false);
            DU_Warning_Input[5].SetActive(true);
        }
        if (du_input1 && du_input2 && du_input3)
        {
            DU_Button.interactable = true;
        }
    }

    public void BackBtnDoubleUp()
    {
        time = initTime;
        Card_Info_BackButton.gameObject.SetActive(false);
        CancelInvoke("DU_setInput2");
        Card_Info_Redeem_Button.interactable = true;
        Card_Info_DU_Button.interactable = true;
        doubleupBack = true;
        DoubleUpPage.SetActive(false);
        CardInfoPage.SetActive(true);
    }
    #endregion

    #region Double Up Spin Page Button
    public void DU_SpinButton()
    {
        time = initTime;
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
        int counter = 24;
        float TimeWait = 0.01f;
        int countmulti = 0;
        if (Voucher_Script.ChosenVoucher == 1)
        {
            countmulti = 3; //1
        }
        else if (Voucher_Script.ChosenVoucher == 2)
        {
            countmulti = 13; //4
        }
        else if (Voucher_Script.ChosenVoucher == 3)
        {
            countmulti = 7; //2
        }
        else if (Voucher_Script.ChosenVoucher == 4)
        {
            countmulti = 16; //5
        }
        else if (Voucher_Script.ChosenVoucher == 5)
        {
            countmulti = 10; //3
        }
        else if (Voucher_Script.ChosenVoucher == 6)
        {
            countmulti = 19; //6
        }
        else if (Voucher_Script.ChosenVoucher == 7)
        {
            countmulti = 22; //7
        }
        else if (Voucher_Script.ChosenVoucher == 8)
        {
            countmulti = 25; //8
        }
        counter *= 5;
        counter += countmulti;
        int slowdown = counter - 24;
        Debug.Log(counter);
        for (int i = 0; i < counter; i++)
        {
            if (DU_Spin_Object.transform.position.y >= 3298.5f)
            {
                DU_Spin_Object.transform.position = new Vector2(DU_Spin_Object.transform.position.x, -1549.5f);
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
        time = initTime;
        SpinPrizePage_DU.SetActive(false);
        VoucherPage_DU.SetActive(true);
    }
    #endregion

    #region Voucher Page Button
    public void DU_VoucherButton()
    {
        time = initTime;
        Voucher_Script.ClearList();
        Voucher_Script.GetData();
        foreach (VoucherEntity s in Voucher_Script.myList)
        {
            if (Voucher_Script.ChosenVoucher == s._id)
            {
                Voucher_Script.MinusUpdateData(s);
                VoucherDistribution_Script.InsertData(phone, Voucher_Script.ChosenVoucher);

                //increase given voucher data
                string given = "given-" + s._type;
                int temp_given = PlayerPrefs.GetInt(given, 0);
                temp_given += 1;
                PlayerPrefs.SetInt(given, temp_given);

                //decrease voucher balance data
                int defaultremain = 0;
                if (s._id == 1)
                {
                    defaultremain = 45;
                }
                else if (s._id == 2)
                {
                    defaultremain = 12;
                }
                else if (s._id == 3)
                {
                    defaultremain = 5;
                }
                else if (s._id == 4)
                {
                    defaultremain = 50;
                }
                else if (s._id == 5)
                {
                    defaultremain = 45;
                }
                else if (s._id == 6)
                {
                    defaultremain = 45;
                }
                else if (s._id == 7)
                {
                    defaultremain = 60;
                }
                else if (s._id == 8)
                {
                    defaultremain = 200;
                }
                string remain = "remaining-" + s._type;
                int temp_remain = PlayerPrefs.GetInt(remain, defaultremain);
                temp_remain -= 1;
                PlayerPrefs.SetInt(remain, temp_remain);
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
        time = initTime;
        Debug.LogError("File name = " + tempvouchername + ".pdf");
        Print_Script.Print(tempvouchername); //printing voucher
    }

    public void OnCompleteAnimationCard()
    {
        SceneManager.LoadScene("Main");
    }
    #endregion
}
