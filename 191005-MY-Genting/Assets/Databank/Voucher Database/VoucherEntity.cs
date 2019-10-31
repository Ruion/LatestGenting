using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class VoucherEntity {

        public int _id;
        public string _type;
        public int _stock;
        public string _dateCreated; // Auto generated timestamp

        public VoucherEntity(string type, int stock)
        {
            _type = type;
            _stock = stock;
			_dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public VoucherEntity(int id, string type, int stock)
        {
            _id = id;
            _type = type;
            _stock = stock;
            _dateCreated = "";
        }

        public VoucherEntity(int id, string type, int stock, string dateCreated)
        {
            _id = id;
            _type = type;
            _stock = stock;
			_dateCreated = dateCreated;
        }
	}
}
