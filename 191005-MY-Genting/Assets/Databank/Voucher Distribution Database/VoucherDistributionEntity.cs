using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
    public class VoucherDistributionEntity
    {

        public int _id;
        public string _userPhone;
        public int _voucherID;
        public string _onlinestatus;
        public string _dateCreated; // Auto generated timestamp

        //for INSERT
        public VoucherDistributionEntity(string userphone, int voucherid)
        {
            _userPhone = userphone;
            _voucherID = voucherid;
            _onlinestatus = "new";
            _dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //for GET
        public VoucherDistributionEntity(int id, string userphone, int voucherid, string onlinestatus, string dateCreated)
        {
            _id = id;
            _userPhone = userphone;
            _voucherID = voucherid;
            _onlinestatus = onlinestatus;
            _dateCreated = dateCreated;
        }
    }
}