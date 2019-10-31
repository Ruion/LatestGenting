using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class VoucherDistributionDb : SqliteHelper
    {
        private const String CodistanTag = "Codistan: VoucherDistributionDb:\t";

        private const String TABLE_NAME = "Voucher_Distribution";
        private const String KEY_ID = "id";
        private const String KEY_USERPHONE = "user_phone";
        private const String KEY_VOUCHERID = "voucher_id";
        private const String KEY_STATUSONLINE = "status_online";
        private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_USERPHONE, KEY_VOUCHERID, KEY_STATUSONLINE, KEY_DATE };

        public VoucherDistributionDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_USERPHONE + " TEXT, " +
                KEY_VOUCHERID + " INTEGER, " +
                KEY_STATUSONLINE + " TEXT, " +
                KEY_DATE + " TEXT )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(VoucherDistributionEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_USERPHONE + ", "
                + KEY_VOUCHERID + ", "
                + KEY_STATUSONLINE + ", "
                + KEY_DATE + " ) "

                + "VALUES ( '"
                + location._userPhone + "', '"
                + location._voucherID.ToString() + "', '"
                + location._onlinestatus + "', '"
                + location._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void updateDataStatus(VoucherDistributionEntity location)
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

            Debug.Log("UPDATE ID : " + location._id);
            Debug.Log("Update Data : " + KEY_STATUSONLINE + ": " + location._onlinestatus);

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
