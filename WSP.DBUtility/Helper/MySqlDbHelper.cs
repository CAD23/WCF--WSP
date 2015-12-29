using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WSP.DBUtility.Helper
{
   public class MySqlDbHelper : IDbHelper
    {
        //连接字符串
        private string strConn = "";
        #region 0--------------------------------构造函数，包含初始化数据库连接字符串
        public MySqlDbHelper()
        {
        }
        public MySqlDbHelper(string conn)
        {
            strConn = conn;
        }
        #endregion


        public string getConn()
        {
            return strConn;
        }

        public System.Data.DataTable GetTableByProc(string ProcName, ref CmdParameter[] parms)
        {
            MySqlParameter[] pas = CFunctions.ConvertToMySqlParameter(parms);
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(ProcName, conn))
                {
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        dt.TableName = ProcName;
                        da.Fill(dt);
                        da.SelectCommand.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + ProcName, ex);
                        throw (ex);
                    }
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = ProcName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (MySqlParameter sp in pas)
                        {
                            cmd.Parameters.Add(sp);
                        }

                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < pas.Length; i++)
                        {
                            pas[i] = cmd.Parameters[i];
                        }
                        parms = CFunctions.RecoverParameter(pas);
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        // throw (ex);
                    }
                }
            }

            return dt;
        }

        public System.Data.DataTable GetTableByProc(string ProcName, CmdParameter[] parms)
        {
            MySqlParameter[] pas = CFunctions.ConvertToMySqlParameter(parms);
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(ProcName, conn))
                {
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        dt.TableName = ProcName;
                        da.Fill(dt);
                        da.SelectCommand.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + ProcName, ex);
                        throw (ex);
                    }
                }
            }

            return dt;
        }

        public System.Data.DataSet GetDataSetByProc(string ProcName, CmdParameter[] parms)
        {
            MySqlParameter[] pas = CFunctions.ConvertToMySqlParameter(parms);
            DataSet dt = new DataSet(); ;
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(ProcName, conn))
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        da.Fill(dt);
                        //command.Parameters["ReturnValue"].Value;
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + ProcName, ex);
                        throw (ex);
                    }

            }
            return dt;
        }

        public int ExcuteProc(string ProcName)
        {
            return ExcuteSQL(ProcName, null, CommandType.StoredProcedure);
        }

        public int ExcuteProc(string ProcName, CmdParameter[] parms)
        {
            MySqlParameter[] pas = CFunctions.ConvertToMySqlParameter(parms);
            return ExcuteSQL(ProcName, pas, CommandType.StoredProcedure);
        }

        public int ExecuteSqlTran(List<string> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (string sql in SQLStringList)
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            int val = cmd.ExecuteNonQuery();
                            // cmd.Parameters.Clear();
                        }
                        trans.Commit();

                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                        return 0;
                        //throw;
                    }
                }
            }
        }



        /// <summary>
        /// 执行非查询存储过程和SQL语句
        /// 增、删、改
        /// </summary>
        /// <param name="strSQL">要执行的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <param name="cmdType">Command类型</param>
        /// <returns>返回影响行数</returns>
        public int ExcuteSQL(string strSQL, MySqlParameter[] paras, CommandType cmdType)
        {
            int i = 0;

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, conn))
                        try
                        {
                            cmd.CommandType = cmdType;
                            if (paras != null)
                            {
                                cmd.Parameters.AddRange(paras);
                            }
                            cmd.Transaction = trans;
                            //  conn.Open();
                            i = cmd.ExecuteNonQuery();
                            trans.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                            trans.Rollback();
                            throw (ex);
                        }

                }
            }
            return i;

        }

    }
}
