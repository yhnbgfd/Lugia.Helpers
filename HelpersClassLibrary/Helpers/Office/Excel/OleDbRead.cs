using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Lugia.Helpers.Office.Excel
{
    class OleDbRead
    {
        /// <summary>
        /// 读取指定xls文件与sheet名的内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataSet ReadXls(string filePath, string sheetName)
        {
            DataSet ds = new DataSet();
            if (!File.Exists(filePath))
            {
                return ds;
            }
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=no;IMEX=1';";
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + sheetName + "$]", strConn);
            try
            {
                oada.Fill(ds);
            }
            catch (Exception ee)
            {
                //Helper.Log.LogHelper.LogError.Error(ee.ToString());
            }
            finally
            {
                oada.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// 读取第一个Sheet的内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet ReadXls(string filePath)
        {
            DataSet ds = new DataSet();
            if (!File.Exists(filePath))
            {
                return ds;
            }
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=no;IMEX=1';";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            var tables = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { });
            var firstTableName = tables.Rows[0]["TABLE_NAME"].ToString();
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + firstTableName + "]", strConn);
            try
            {
                oada.Fill(ds);
            }
            catch (Exception ee)
            {
                //Helper.Log.LogHelper.LogError.Error(ee.ToString());
            }
            finally
            {
                oada.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return ds;
        }
    }
}
