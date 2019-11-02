using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UI;
using TMPro;
using System;

public class StockController : MonoBehaviour
{
    public VoucherDatabase voucherdatabase;
    public TMP_InputField[] Input_Field_Stock;
    public List<VoucherEntity> myList = new List<VoucherEntity>();

    public StockChangesSummary stockmonitor;
    public VoucherGivenSummary voucher_given_database;
    public VoucherRemainingSummary voucher_remaining_database;

    public Button Setbtn;
    public Button Savebtn;

    public GameObject nomorevoucher;
    // Start is called before the first frame update
    void Start()
    {
        Input_Field_Stock[0].text = PlayerPrefs.GetInt("TheBackeryRM10", 45).ToString();
        Input_Field_Stock[1].text = PlayerPrefs.GetInt("MedanSeleraRM20", 12).ToString();
        Input_Field_Stock[2].text = PlayerPrefs.GetInt("FuHuRM50", 5).ToString();
        Input_Field_Stock[3].text = PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50).ToString();
        Input_Field_Stock[4].text = PlayerPrefs.GetInt("GongChaFree1", 45).ToString();
        Input_Field_Stock[5].text = PlayerPrefs.GetInt("SanFranciscoFree1", 45).ToString();

        /*voucherdatabase.ClearList();
        voucherdatabase.GetData();
        int s = 0;
        foreach(VoucherEntity aa in voucherdatabase.myList)
        {
            if(aa._stock <= 0)
            {
                s += 1;
            }
        }
        if(s >= 6)
        {
            nomorevoucher.SetActive(true);
        }*/
    }

    public void GetStock()
    {
        Input_Field_Stock[0].text = PlayerPrefs.GetInt("TheBackeryRM10", 45).ToString();
        Input_Field_Stock[1].text = PlayerPrefs.GetInt("MedanSeleraRM20", 12).ToString();
        Input_Field_Stock[2].text = PlayerPrefs.GetInt("FuHuRM50", 5).ToString();
        Input_Field_Stock[3].text = PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50).ToString();
        Input_Field_Stock[4].text = PlayerPrefs.GetInt("GongChaFree1", 45).ToString();
        Input_Field_Stock[5].text = PlayerPrefs.GetInt("SanFranciscoFree1", 45).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerPref()
    {
        PlayerPrefs.SetInt("given-TheBackeryRM10", 0);
        PlayerPrefs.SetInt("given-MedanSeleraRM20", 0);
        PlayerPrefs.SetInt("given-FuHuRM50", 0);
        PlayerPrefs.SetInt("given-MoltenChocolateBuy1Free2", 0);
        PlayerPrefs.SetInt("given-GongChaFree1", 0);
        PlayerPrefs.SetInt("given-SanFranciscoFree1", 0);

        Debug.LogError("Player given summary : " + PlayerPrefs.GetInt("given-TheBackeryRM10") + " | " + PlayerPrefs.GetInt("remaining-MedanSeleraRM20") + " | " + PlayerPrefs.GetInt("given-FuHuRM50") + " | " + PlayerPrefs.GetInt("given-MoltenChocolateBuy1Free2") + " | " + PlayerPrefs.GetInt("given-GongChaFree1") + " | " + PlayerPrefs.GetInt("given-SanFranciscoFree1"));

        PlayerPrefs.SetInt("remaining-TheBackeryRM10", PlayerPrefs.GetInt("TheBackeryRM10", 45));
        PlayerPrefs.SetInt("remaining-MedanSeleraRM20", PlayerPrefs.GetInt("MedanSeleraRM20", 12));
        PlayerPrefs.SetInt("remaining-FuHuRM50", PlayerPrefs.GetInt("FuHuRM50", 5));
        PlayerPrefs.SetInt("remaining-MoltenChocolateBuy1Free2", PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50));
        PlayerPrefs.SetInt("remaining-GongChaFree1", PlayerPrefs.GetInt("GongChaFree1", 45));
        PlayerPrefs.SetInt("remaining-SanFranciscoFree1", PlayerPrefs.GetInt("SanFranciscoFree1", 45));
    }
    public void SetStock()
    {
        string temp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //given voucher
        int g_v1 = PlayerPrefs.GetInt("given-TheBackeryRM10", 0);
        int g_v2 = PlayerPrefs.GetInt("given-MedanSeleraRM20", 0);
        int g_v3 = PlayerPrefs.GetInt("given-FuHuRM50", 0);
        int g_v4 = PlayerPrefs.GetInt("given-MoltenChocolateBuy1Free2", 0);
        int g_v5 = PlayerPrefs.GetInt("given-GongChaFree1", 0);
        int g_v6 = PlayerPrefs.GetInt("given-SanFranciscoFree1", 0);
        voucher_given_database.InsertData(g_v1, g_v2, g_v3, g_v4, g_v5, g_v6, temp);

        //remaining voucher
        int r_v1 = PlayerPrefs.GetInt("remaining-TheBackeryRM10", PlayerPrefs.GetInt("TheBackeryRM10", 45));
        int r_v2 = PlayerPrefs.GetInt("remaining-MedanSeleraRM20", PlayerPrefs.GetInt("MedanSeleraRM20", 12));
        int r_v3 = PlayerPrefs.GetInt("remaining-FuHuRM50", PlayerPrefs.GetInt("FuHuRM50", 5));
        int r_v4 = PlayerPrefs.GetInt("remaining-MoltenChocolateBuy1Free2", PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50));
        int r_v5 = PlayerPrefs.GetInt("remaining-GongChaFree1", PlayerPrefs.GetInt("GongChaFree1", 45));
        int r_v6 = PlayerPrefs.GetInt("remaining-SanFranciscoFree1", PlayerPrefs.GetInt("SanFranciscoFree1", 45));
        voucher_remaining_database.InsertData(r_v1, r_v2, r_v3, r_v4, r_v5, r_v6, temp);

        SetPlayerPref();

        //Change daily reset stock
        PlayerPrefs.SetInt("TheBackeryRM10", int.Parse(Input_Field_Stock[0].text));
        PlayerPrefs.SetInt("MedanSeleraRM20", int.Parse(Input_Field_Stock[1].text));
        PlayerPrefs.SetInt("FuHuRM50", int.Parse(Input_Field_Stock[2].text));
        PlayerPrefs.SetInt("MoltenChocolateBuy1Free2", int.Parse(Input_Field_Stock[3].text));
        PlayerPrefs.SetInt("GongChaFree1", int.Parse(Input_Field_Stock[4].text));
        PlayerPrefs.SetInt("SanFranciscoFree1", int.Parse(Input_Field_Stock[5].text));

        //Stock monitor database
        stockmonitor.InsertData(int.Parse(Input_Field_Stock[0].text), int.Parse(Input_Field_Stock[1].text), int.Parse(Input_Field_Stock[2].text), int.Parse(Input_Field_Stock[3].text), int.Parse(Input_Field_Stock[4].text), int.Parse(Input_Field_Stock[5].text), temp);
    }
    public void SaveStock()
    {
        voucherdatabase.ClearList();
        voucherdatabase.GetData();
        foreach (VoucherEntity s in voucherdatabase.myList)
        {
            if (s._id == 1)
            {
                s._stock = int.Parse(Input_Field_Stock[0].text);
            }
            else if (s._id == 2)
            {
                s._stock = int.Parse(Input_Field_Stock[1].text);
            }
            else if (s._id == 3)
            {
                s._stock = int.Parse(Input_Field_Stock[2].text);
            }
            else if (s._id == 4)
            {
                s._stock = int.Parse(Input_Field_Stock[3].text);
            }
            else if (s._id == 5)
            {
                s._stock = int.Parse(Input_Field_Stock[4].text);
            }
            else if (s._id == 6)
            {
                s._stock = int.Parse(Input_Field_Stock[5].text);
            }
            myList.Add(s);
        }
        foreach (VoucherEntity a in myList)
        {
            voucherdatabase.UpdateStockData(a);
        }
    }

    public void CheckEmptyInput()
    {
        if(Input_Field_Stock[0].text != "" && Input_Field_Stock[1].text != "" && Input_Field_Stock[2].text != "" && Input_Field_Stock[3].text != "" && Input_Field_Stock[4].text != "" && Input_Field_Stock[5].text != "")
        {
            Setbtn.interactable = true;
            Savebtn.interactable = true;
        }
        else
        {
            Setbtn.interactable = false;
            Savebtn.interactable = false;
        }
    }
}
