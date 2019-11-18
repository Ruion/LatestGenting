using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;
using System.IO;
using UnityEngine.UI;

public class VoucherGivenSummary : MonoBehaviour
{
    public List<VoucherGivenEntity> myList = new List<VoucherGivenEntity>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertData(int voucher1, int voucher2, int voucher3, int voucher4, int voucher5, int voucher6, int voucher7, int voucher8, string datetime)
    {
        VoucherGivenDb mLocationDb = new VoucherGivenDb();
        mLocationDb.addData(new VoucherGivenEntity(voucher1, voucher2, voucher3, voucher4, voucher5, voucher6, voucher7, voucher8, datetime));
        mLocationDb.close();
    }

    public void UpdateData()
    {

    }

    public void ClearList()
    {
        myList = new List<VoucherGivenEntity>();
    }
}
