using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UI;
using TMPro;

public class StockController : MonoBehaviour
{
    public VoucherDatabase voucherdatabase;
    public Text[] Input_Field_Stock;
    public List<VoucherEntity> myList = new List<VoucherEntity>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStock()
    {
        PlayerPrefs.SetInt("TheBackeryRM10", int.Parse(Input_Field_Stock[0].text));
        PlayerPrefs.SetInt("MedanSeleraRM20", int.Parse(Input_Field_Stock[1].text));
        PlayerPrefs.SetInt("FuHuRM50", int.Parse(Input_Field_Stock[2].text));
        PlayerPrefs.SetInt("MoltenChocolateBuy1Free2", int.Parse(Input_Field_Stock[3].text));
        PlayerPrefs.SetInt("GongChaFree1", int.Parse(Input_Field_Stock[4].text));
        PlayerPrefs.SetInt("SanFranciscoFree1", int.Parse(Input_Field_Stock[5].text));
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
        foreach(VoucherEntity a in myList)
        {
            voucherdatabase.UpdateStockData(a);
        }
    }
}
