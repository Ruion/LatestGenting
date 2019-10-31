using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class UserDb : SqliteHelper
    {
        private const String CodistanTag = "Codistan: UserDb:\t";

        private const String TABLE_NAME = "User";
        private const String KEY_ID = "id";
        private const String KEY_NAME = "name";
        private const String KEY_PHONE = "phone";
        private const String KEY_EMAIL = "email";
        private const String KEY_STATUS = "status";
        private const String KEY_MEMBERID = "card_number";
        private const String KEY_REFERENCECODE = "reference_code";
        private const String KEY_STATUSONLINE = "status_online";
        private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_NAME, KEY_PHONE, KEY_DATE };

        public UserDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_NAME + " TEXT, " +
                KEY_PHONE + " TEXT, " +
                KEY_EMAIL + " TEXT, " +
                KEY_STATUS + " TEXT, " +
                KEY_MEMBERID + " TEXT, " +
                KEY_REFERENCECODE + " TEXT, " +
                KEY_STATUSONLINE + " TEXT, " +
                KEY_DATE + " TEXT )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(UserEntity user)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_NAME + ", "
                + KEY_PHONE + ", "
                + KEY_EMAIL + ", "
                + KEY_STATUS + ", "
                + KEY_MEMBERID + ", "
                + KEY_REFERENCECODE + ", "
                + KEY_STATUSONLINE + ", "
                + KEY_DATE + " ) "

                + "VALUES ( '"
                + user._name + "', '"
                + user._phone + "', '"
                + user._email + "', '"
                + user._status + "', '"
                + user._memberid + "', '"
                + user._referencecode + "', '"
                + user._onlinestatus + "', '"
                + user._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void updateData(UserEntity user)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME
                + " SET "
                + KEY_STATUSONLINE + " = '"
                + "submitted" + "' "
                + "WHERE "
                + KEY_ID + " = '"
                + user._id.ToString() + "'";

            try { dbcmd.ExecuteNonQuery(); Debug.LogError("update success"); }
            catch (System.Exception ex) { Debug.Log(ex.Message); }
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(CodistanTag + "Getting user: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_STATUSONLINE + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(CodistanTag + "Deleting user: " + id);

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
