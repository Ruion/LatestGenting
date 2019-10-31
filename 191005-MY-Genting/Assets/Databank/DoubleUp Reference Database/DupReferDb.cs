using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class DupReferDb : SqliteHelper
    {
        private const String CodistanTag = "Codistan: DupReferDb:\t";

        private const String TABLE_NAME = "Dup_Reference";
        private const String KEY_ID = "id";
        private const String KEY_USERPHONE = "user_phone";
        private const String KEY_NAME = "name";
        private const String KEY_PHONE = "phone";
        private const String KEY_EMAIL = "email";
        private const String KEY_STATUSONLINE = "status_online";
        private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_USERPHONE, KEY_NAME, KEY_PHONE, KEY_EMAIL, KEY_STATUSONLINE, KEY_DATE };

        public DupReferDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_USERPHONE + " TEXT, " +
                KEY_NAME + " TEXT, " +
                KEY_PHONE + " TEXT, " +
                KEY_EMAIL + " TEXT, " +
                KEY_STATUSONLINE + " TEXT, " +
                KEY_DATE + " TEXT )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(DupReferEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_USERPHONE + ", "
                + KEY_NAME + ", "
                + KEY_PHONE + ", "
                + KEY_EMAIL + ", "
                + KEY_STATUSONLINE + ", "
                + KEY_DATE + " ) "

                + "VALUES ( '"
                + location._userphone + "', '"
                + location._name + "', '"
                + location._phone + "', '"
                + location._email + "', '"
                + location._onlinestatus + "', '"
                + location._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void updateDataStatus(DupReferEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME
                + " SET "
                + KEY_STATUSONLINE + " = '"
                + "submitted" + "' "
                + "WHERE "
                + KEY_ID + " = '"
                + location._id + "'";

            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(CodistanTag + "Getting Location: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_STATUSONLINE + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(CodistanTag + "Deleting Location: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int id)
        {
            base.deleteDataById(id);
        }

        public override void deleteAllData()
        {
            Debug.Log(CodistanTag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }

        public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }
    }
}
