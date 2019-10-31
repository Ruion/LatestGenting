using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;
using System.IO;
using UnityEngine.UI;

public class VoucherDistributionDatabase : MonoBehaviour
{
    public List<VoucherDistributionEntity> myList = new List<VoucherDistributionEntity>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //to INSERT DATA
    public void InsertData(string userphone, int voucherid)
    {
        VoucherDistributionDb mVoucherDb = new VoucherDistributionDb();
        mVoucherDb.addData(new VoucherDistributionEntity(userphone, voucherid));
        mVoucherDb.close();
    }

    //get all data that has status new
    public void GetDataByStatus()
    {
        VoucherDistributionDb mVoucherDb2 = new VoucherDistributionDb();
        System.Data.IDataReader reader = mVoucherDb2.getDataByString("new");
        while (reader.Read())
        {
            VoucherDistributionEntity entity = new VoucherDistributionEntity(int.Parse(reader[0].ToString()),
                                                        reader[1].ToString(),
                                                        int.Parse(reader[2].ToString()),
                                                        reader[3].ToString(),
                                                        reader[4].ToString());

            Debug.Log("ID: " + entity._id + " & phone: " + entity._userPhone + " & voucherid: " + entity._voucherID + " & status: " + entity._onlinestatus);
            myList.Add(entity);
        }
        mVoucherDb2.close();
    }

    //get all data
    public void GetAllData()
    {
        VoucherDistributionDb mVoucherDb2 = new VoucherDistributionDb();
        System.Data.IDataReader reader = mVoucherDb2.getAllData();
        while (reader.Read())
        {
            VoucherDistributionEntity entity = new VoucherDistributionEntity(int.Parse(reader[0].ToString()),
                                                        reader[1].ToString(),
                                                        int.Parse(reader[2].ToString()),
                                                        reader[3].ToString(),
                                                        reader[4].ToString());

            Debug.Log("ID: " + entity._id + " & phone: " + entity._userPhone + " & voucherid: " + entity._voucherID + " & statu: " + entity._onlinestatus);
            myList.Add(entity);
        }
        mVoucherDb2.close();
    }

    //update status to submitted
    public void UpdateOnlineStatusData(VoucherDistributionEntity s)
    {
        s._onlinestatus = "submitted";
        VoucherDistributionDb mVoucherDb = new VoucherDistributionDb();
        mVoucherDb.updateDataStatus(s);
        mVoucherDb.close();
    }

    //reset list
    public void ClearList()
    {
        myList = new List<VoucherDistributionEntity>();
    }
}
