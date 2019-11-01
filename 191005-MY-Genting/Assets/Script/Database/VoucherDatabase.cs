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
                    ResetDailyData(ve);
                }
                string todaydate = DateTime.Now.Date.ToString();
                PlayerPrefs.SetString("TheDate", todaydate);
            }

        }
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
