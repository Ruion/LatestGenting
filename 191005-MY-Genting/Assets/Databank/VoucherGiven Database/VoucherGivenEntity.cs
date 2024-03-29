﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
    public class VoucherGivenEntity
    {

        public int _id;
        public int _voucher1;
        public int _voucher2;
        public int _voucher3;
        public int _voucher4;
        public int _voucher5;
        public int _voucher6;
        public string _dateCreated; // Auto generated timestamp

        //INSERT Data
        public VoucherGivenEntity(int voucher1, int voucher2, int voucher3, int voucher4, int voucher5, int voucher6)
        {
            _voucher1 = voucher1;
            _voucher2 = voucher2;
            _voucher3 = voucher3;
            _voucher4 = voucher4;
            _voucher5 = voucher5;
            _voucher6 = voucher6;
            _dateCreated = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //GET Data
        public VoucherGivenEntity(int id, int voucher1, int voucher2, int voucher3, int voucher4, int voucher5, int voucher6, string dateCreated)
        {
            _id = id;
            _voucher1 = voucher1;
            _voucher2 = voucher2;
            _voucher3 = voucher3;
            _voucher4 = voucher4;
            _voucher5 = voucher5;
            _voucher6 = voucher6;
            _dateCreated = dateCreated;
        }
    }
}
