using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;
using System.IO;
using UnityEngine.UI;

public class VoucherDatabase : MonoBehaviour
{
    public int ChosenVoucher;
    public VoucherEntity ChosenVoucherEntity;
    public List<VoucherEntity> myList = new List<VoucherEntity>();

    public VoucherGivenSummary voucher_given_database;
    public VoucherRemainingSummary voucher_remaining_database;

    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(Application.dataPath + "/GENTING_db"))
        {
            //INSERT DATA
            VoucherDb mVoucherDb = new VoucherDb();
            mVoucherDb.addData(new VoucherEntity("TheBackeryRM10", PlayerPrefs.GetInt("TheBackeryRM10", 45)));
            mVoucherDb.addData(new VoucherEntity("MedanSeleraRM20", PlayerPrefs.GetInt("MedanSeleraRM20", 12)));
            mVoucherDb.addData(new VoucherEntity("FuHuRM50", PlayerPrefs.GetInt("FuHuRM50", 5)));
            mVoucherDb.addData(new VoucherEntity("MoltenChocolateBuy1Free2", PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50)));
            mVoucherDb.addData(new VoucherEntity("GongChaFree1", PlayerPrefs.GetInt("GongChaFree1", 45)));
            mVoucherDb.addData(new VoucherEntity("SanFranciscoFree1", PlayerPrefs.GetInt("SanFranciscoFree1", 45)));
            mVoucherDb.addData(new VoucherEntity("Evian", PlayerPrefs.GetInt("Evian", 60)));
            mVoucherDb.addData(new VoucherEntity("XingFuTang", PlayerPrefs.GetInt("XingFuTang", 200)));
            mVoucherDb.close();
        }
        CheckDay();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CheckDay()
    {
        string temp = PlayerPrefs.GetString("TheDate", "");
        if (temp == "")
        {
            string todaydate = DateTime.Now.Date.ToString();
            PlayerPrefs.SetString("TheDate", todaydate);
        }
        else
        {
            DateTime Date1 = DateTime.Parse(temp);
            DateTime Date2 = DateTime.Now.Date;

            int a = DateTime.Compare(Date1, Date2);
            if (a < 0)
            {
                ClearList();
                GetData();
                foreach (VoucherEntity ve in myList)
                {
                    if (ve._type == "TheBackeryRM10")
                    {
                        ve._stock = PlayerPrefs.GetInt("TheBackeryRM10", 45);
                    }
                    else if (ve._type == "MedanSeleraRM20")
                    {
                        ve._stock = PlayerPrefs.GetInt("MedanSeleraRM20", 12);
                    }
                    else if (ve._type == "FuHuRM50")
                    {
                        ve._stock = PlayerPrefs.GetInt("FuHuRM50", 5);
                    }
                    else if (ve._type == "MoltenChocolateBuy1Free2")
                    {
                        ve._stock = PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50);
                    }
                    else if (ve._type == "GongChaFree1")
                    {
                        ve._stock = PlayerPrefs.GetInt("GongChaFree1", 45);
                    }
                    else if (ve._type == "SanFranciscoFree1")
                    {
                        ve._stock = PlayerPrefs.GetInt("SanFranciscoFree1", 45);
                    }
                    else if (ve._type == "Evian")
                    {
                        ve._stock = PlayerPrefs.GetInt("Evian", 60);
                    }
                    else if (ve._type == "XingFuTang")
                    {
                        ve._stock = PlayerPrefs.GetInt("XingFuTang", 200);
                    }
                    ResetDailyData(ve);
                }
                //given voucher
                int g_v1 = PlayerPrefs.GetInt("given-TheBackeryRM10", 0);
                int g_v2 = PlayerPrefs.GetInt("given-MedanSeleraRM20", 0);
                int g_v3 = PlayerPrefs.GetInt("given-FuHuRM50", 0);
                int g_v4 = PlayerPrefs.GetInt("given-MoltenChocolateBuy1Free2", 0);
                int g_v5 = PlayerPrefs.GetInt("given-GongChaFree1", 0);
                int g_v6 = PlayerPrefs.GetInt("given-SanFranciscoFree1", 0);
                int g_v7 = PlayerPrefs.GetInt("given-Evian", 0);
                int g_v8 = PlayerPrefs.GetInt("given-XingFuTang", 0);
                voucher_given_database.InsertData(g_v1, g_v2, g_v3, g_v4, g_v5, g_v6, g_v7, g_v8, temp);

                //remaining voucher
                int r_v1 = PlayerPrefs.GetInt("remaining-TheBackeryRM10", PlayerPrefs.GetInt("TheBackeryRM10", 45));
                int r_v2 = PlayerPrefs.GetInt("remaining-MedanSeleraRM20", PlayerPrefs.GetInt("MedanSeleraRM20", 12));
                int r_v3 = PlayerPrefs.GetInt("remaining-FuHuRM50", PlayerPrefs.GetInt("FuHuRM50", 5));
                int r_v4 = PlayerPrefs.GetInt("remaining-MoltenChocolateBuy1Free2", PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50));
                int r_v5 = PlayerPrefs.GetInt("remaining-GongChaFree1", PlayerPrefs.GetInt("GongChaFree1", 45));
                int r_v6 = PlayerPrefs.GetInt("remaining-SanFranciscoFree1", PlayerPrefs.GetInt("SanFranciscoFree1", 45));
                int r_v7 = PlayerPrefs.GetInt("remaining-Evian", PlayerPrefs.GetInt("Evian", 60));
                int r_v8 = PlayerPrefs.GetInt("remaining-XingFuTang", PlayerPrefs.GetInt("XingFuTang", 200));
                voucher_remaining_database.InsertData(r_v1, r_v2, r_v3, r_v4, r_v5, r_v6, r_v7, r_v8, temp);

                //reset playerpref
                SetPlayerpref();

                string todaydate = DateTime.Now.Date.ToString();
                PlayerPrefs.SetString("TheDate", todaydate);
            }

        }
    }

    public void SetPlayerpref()
    {
        PlayerPrefs.SetInt("given-TheBackeryRM10", 0);
        PlayerPrefs.SetInt("given-MedanSeleraRM20", 0);
        PlayerPrefs.SetInt("given-FuHuRM50", 0);
        PlayerPrefs.SetInt("given-MoltenChocolateBuy1Free2", 0);
        PlayerPrefs.SetInt("given-GongChaFree1", 0);
        PlayerPrefs.SetInt("given-SanFranciscoFree1", 0);
        PlayerPrefs.SetInt("given-Evian", 0);
        PlayerPrefs.SetInt("given-XingFuTang", 0);


        PlayerPrefs.SetInt("remaining-TheBackeryRM10", PlayerPrefs.GetInt("TheBackeryRM10", 45));
        PlayerPrefs.SetInt("remaining-MedanSeleraRM20", PlayerPrefs.GetInt("MedanSeleraRM20", 12));
        PlayerPrefs.SetInt("remaining-FuHuRM50", PlayerPrefs.GetInt("FuHuRM50", 5));
        PlayerPrefs.SetInt("remaining-MoltenChocolateBuy1Free2", PlayerPrefs.GetInt("MoltenChocolateBuy1Free2", 50));
        PlayerPrefs.SetInt("remaining-GongChaFree1", PlayerPrefs.GetInt("GongChaFree1", 45));
        PlayerPrefs.SetInt("remaining-SanFranciscoFree1", PlayerPrefs.GetInt("SanFranciscoFree1", 45));
        PlayerPrefs.SetInt("remaining-Evian", PlayerPrefs.GetInt("Evian", 60));
        PlayerPrefs.SetInt("remaining-XingFuTang", PlayerPrefs.GetInt("XingFuTang", 200));
    }

    public void GetData()
    {
        //GET DATA (FETCH)
        VoucherDb mVoucherDb2 = new VoucherDb();
        System.Data.IDataReader reader = mVoucherDb2.getAllData();
        while (reader.Read())
        {
            VoucherEntity entity = new VoucherEntity(int.Parse(reader[0].ToString()),
                                                        reader[1].ToString(),
                                                        int.Parse(reader[2].ToString()),
                                                        reader[3].ToString());

            Debug.Log("ID: " + entity._id + " & Type: " + entity._type + " & Stock: " + entity._stock);
            myList.Add(entity);
        }
        mVoucherDb2.close();
    }

    public void ResetDailyData(VoucherEntity u)
    {
        VoucherDb mVoucherDb3 = new VoucherDb();
        mVoucherDb3.updateData(u);
        mVoucherDb3.close();
    }

    public void MinusUpdateData(VoucherEntity i)
    {
        i._stock -= 1;
        //UPDATE DATA
        VoucherDb mVoucherDb3 = new VoucherDb();
        mVoucherDb3.updateData(i);
        mVoucherDb3.close();
    }
    public void PlusUpdateData(VoucherEntity i)
    {
        i._stock += 1;
        //UPDATE DATA
        VoucherDb mVoucherDb3 = new VoucherDb();
        mVoucherDb3.updateData(i);
        mVoucherDb3.close();
    }

    public void UpdateStockData(VoucherEntity d)
    {
        VoucherDb mVoucherDb3 = new VoucherDb();
        mVoucherDb3.updateData(d);
        mVoucherDb3.close();
    }

    public void ClearList()
    {
        myList = new List<VoucherEntity>();
    }
}
