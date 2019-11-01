using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System;
using System.IO;
using UnityEngine.UI;

public class VoucherRemainingSummary : MonoBehaviour
{
    public List<VoucherRemainingEntity> myList = new List<VoucherRemainingEntity>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertData(int voucher1, int voucher2, int voucher3, int voucher4, int voucher5, int voucher6, string datetime)
    {
        VoucherRemainingDb mLocationDb = new VoucherRemainingDb();
        mLocationDb.addData(new VoucherRemainingEntity(voucher1, voucher2, voucher3, voucher4, voucher5, voucher6, datetime));
        mLocationDb.close();
    }

    public void ClearList()
    {
        myList = new List<VoucherRemainingEntity>();
    }
}
