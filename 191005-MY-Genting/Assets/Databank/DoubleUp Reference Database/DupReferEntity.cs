using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
    public class DupReferEntity
    {

        public int _id;
        public string _userphone;
        public string _name;
        public string _phone;
        public string _email;
        public string _onlinestatus;
        public string _dateCreated; // Auto generated timestamp

        //for INSERT
        public DupReferEntity(string userphone, string name, string phone, string email)
        {
            _userphone = userphone;
            _name = name;
            _phone = phone;
            _email = email;
            _onlinestatus = "new";
            _dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //for GET
        public DupReferEntity(int id, string userphone, string name, string phone, string email, string onlinestatus, string dateCreated)
        {
            _id = id;
            _userphone = userphone;
            _name = name;
            _phone = phone;
            _email = email;
            _onlinestatus = onlinestatus;
            _dateCreated = dateCreated;
        }
    }
}