using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank{
	public class VoucherDb : SqliteHelper {
		private const String CodistanTag = "Codistan: VoucherDb:\t";
        
        private const String TABLE_NAME = "Voucher_Stock";
        private const String KEY_ID = "id";
        private const String KEY_TYPE = "type";
        private const String KEY_STOCK = "stock";
		private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] {KEY_ID, KEY_TYPE, KEY_STOCK, KEY_DATE};

        public VoucherDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_TYPE + " TEXT, " +
                KEY_STOCK + " INTEGER, " +
                KEY_DATE + " DATETIME DEFAULT CURRENT_TIMESTAMP )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(VoucherEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_TYPE + ", "
                + KEY_STOCK + ", "
                + KEY_DATE + " ) "

                + "VALUES ( '"
                + location._type + "', '"
                + location._stock.ToString() + "', '"
                + location._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void updateData(VoucherEntity location)
        {

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME
                + " SET " 
                + KEY_STOCK + " = '" 
                + location._stock.ToString() + "' " 
                + "WHERE " 
                + KEY_ID + " = '" 
                + location._id.ToString() + "'";

            Debug.Log("Update Data : " + KEY_STOCK + ": " + location._stock);
            Debug.Log("UPDATE ID : " + location._id);

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
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + str + "'";
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
