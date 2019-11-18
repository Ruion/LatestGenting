using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class StockChangesDb : SqliteHelper
    {
        private const String CodistanTag = "Codistan: StockChangesDb:\t";

        private const String TABLE_NAME = "Stock_Changes_Summary";
        private const String KEY_ID = "id";
        private const String KEY_VOUCHER1 = "TheBakeryRM10";
        private const String KEY_VOUCHER2 = "MedanSeleraRM20";
        private const String KEY_VOUCHER3 = "FuHuRM50";
        private const String KEY_VOUCHER4 = "MoltenChocolateBuy1Free2";
        private const String KEY_VOUCHER5 = "GongChaFree1";
        private const String KEY_VOUCHER6 = "SanFranciscoFree1";
        private const String KEY_VOUCHER7 = "Evian";
        private const String KEY_VOUCHER8 = "XingFuTang";
        private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_VOUCHER1, KEY_VOUCHER2, KEY_VOUCHER3, KEY_VOUCHER4, KEY_VOUCHER5, KEY_VOUCHER6, KEY_VOUCHER7, KEY_VOUCHER8, KEY_DATE };

        public StockChangesDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_VOUCHER1 + " INTEGER, " +
                KEY_VOUCHER2 + " INTEGER, " +
                KEY_VOUCHER3 + " INTEGER, " +
                KEY_VOUCHER4 + " INTEGER, " +
                KEY_VOUCHER5 + " INTEGER, " +
                KEY_VOUCHER6 + " INTEGER, " +
                KEY_VOUCHER7 + " INTEGER, " +
                KEY_VOUCHER8 + " INTEGER, " +
                KEY_DATE + " TEXT )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(StockChangesEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_VOUCHER1 + ", "
                + KEY_VOUCHER2 + ", "
                + KEY_VOUCHER3 + ", "
                + KEY_VOUCHER4 + ", "
                + KEY_VOUCHER5 + ", "
                + KEY_VOUCHER6 + ", "
                + KEY_VOUCHER7 + ", "
                + KEY_VOUCHER8 + ", "
                + KEY_DATE + " ) "

                + "VALUES ( '"
                + location._voucher1 + "', '"
                + location._voucher2 + "', '"
                + location._voucher3 + "', '"
                + location._voucher4 + "', '"
                + location._voucher5 + "', '"
                + location._voucher6 + "', '"
                + location._voucher7 + "', '"
                + location._voucher8 + "', '"
                + location._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void updateData(StockChangesEntity location)
        {

            /*IDbCommand dbcmd = getDbCommand();
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

            dbcmd.ExecuteNonQuery();*/
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
