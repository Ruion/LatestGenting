using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;
using System.IO;
using UnityEngine.UI;

public class DupReferDatabase : MonoBehaviour
{
    public List<DupReferEntity> myList = new List<DupReferEntity>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertData(string userphone, string name, string phone, string email)
    {
        DupReferDb mVoucherDb = new DupReferDb();
        mVoucherDb.addData(new DupReferEntity(userphone, name, phone, email));
        mVoucherDb.close();
    }

    public void GetDataByOnlineStatus()
    {
        DupReferDb mVoucherDb2 = new DupReferDb();
        System.Data.IDataReader reader = mVoucherDb2.getDataByString("new");
        while (reader.Read())
        {
            DupReferEntity entity = new DupReferEntity(int.Parse(reader[0].ToString()),
                                                        reader[1].ToString(),
                                                        reader[2].ToString(),
                                                        reader[3].ToString(),
                                                        reader[4].ToString(),
                                                        reader[5].ToString(),
                                                        reader[6].ToString());

            Debug.Log("Doubleup Refer --> ID: " + reader[0] + " & user phone: " + reader[1] + " & name: " + reader[2] + " & phone: " + reader[3] + " & email: " + reader[4]);
            myList.Add(entity);
        }
        mVoucherDb2.close();
    }

    public void GetAllData()
    {
        DupReferDb mVoucherDb2 = new DupReferDb();
        System.Data.IDataReader reader = mVoucherDb2.getAllData();
        while (reader.Read())
        {
            DupReferEntity entity = new DupReferEntity(int.Parse(reader[0].ToString()),
                                                        reader[1].ToString(),
                                                        reader[2].ToString(),
                                                        reader[3].ToString(),
                                                        reader[4].ToString(),
                                                        reader[5].ToString(),
                                                        reader[6].ToString());

            Debug.Log("Doubleup Refer --> ID: " + reader[0] + " & user phone: " + reader[1] + " & name: " + reader[2] + " & phone: " + reader[3] + " & email: " + reader[4]);
            myList.Add(entity);
        }
        mVoucherDb2.close();
    }

    public void UpdateOnlineStatusData(DupReferEntity s)
    {
        s._onlinestatus = "submitted";
        DupReferDb mVoucherDb = new DupReferDb();
        mVoucherDb.updateDataStatus(s);
        mVoucherDb.close();
    }

    public void ClearList()
    {
        myList = new List<DupReferEntity>();
    }
}
