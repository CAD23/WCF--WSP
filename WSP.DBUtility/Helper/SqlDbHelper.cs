using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WSP.DBUtility
{
    public class SqlDbHelper : IDbHelper
    {
        //连接字符串
        private  string strConn = "";


        #region 0--------------------------------构造函数，包含初始化数据库连接字符串

        public SqlDbHelper()
        {

        }
        public SqlDbHelper(string conn)
        {
            //strConn = ConfigurationManager.ConnectionStrings[dbname].ToString();
            strConn = conn;
        }

        #endregion


        #region 1-------------------------------获取连接字符串
        public string getConn()
        {
            return strConn;
        }
        #endregion


        #region 2----------------------------执行存储过程查询，返回DataTable对象

        /// <summary>
        /// 根据存储过程获取DataTable，参数的修改会被返回
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetTableByProc(string ProcName, ref CmdParameter[] parms)
        {
            SqlParameter[] pas = CFunctions.ConvertToSqlParameter(parms);
            DataTable dt = new DataTable(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(ProcName, conn))
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
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = ProcName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (SqlParameter sp in pas)
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
                    catch (Exception ex) {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                       // throw (ex);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据存储过程获取DataTable
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetTableByProc(string ProcName, CmdParameter[] parms)
        {
            SqlParameter[] pas = CFunctions.ConvertToSqlParameter(parms);
            DataTable dt = new DataTable(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(ProcName, conn))
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
            return dt;
        }

        #endregion


        #region 3---------------------------执行查询，返回DataSet对象
        
        public DataSet GetDataSetByProc(string ProcName, CmdParameter[] parms)
        {
            SqlParameter[] pas = CFunctions.ConvertToSqlParameter(parms);
            DataSet dt = new DataSet(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(ProcName, conn))
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

        #endregion


        #region 4---------------------------执行非查询存储过程和SQL语句

        public int ExcuteProc(string ProcName)
        {

            return ExcuteSQL(ProcName, null, CommandType.StoredProcedure);
        }

        public int ExcuteProc(string ProcName, CmdParameter[] parms)
        {
            SqlParameter[] pas = CFunctions.ConvertToSqlParameter(parms);
            return ExcuteSQL(ProcName, pas, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行非查询存储过程和SQL语句
        /// 增、删、改
        /// </summary>
        /// <param name="strSQL">要执行的SQL语句</param>
        /// <param name="paras">参数列表，没有参数填入null</param>
        /// <param name="cmdType">Command类型</param>
        /// <returns>返回影响行数</returns>
        public int ExcuteSQL(string strSQL, SqlParameter[] paras, CommandType cmdType)
        {
            int i = 0;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL, conn))
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

        #endregion


        #region 6---------------------------批量执行SQL语句

        public int ExecuteSqlTran(List<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
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
                    catch(Exception ex)
                    {
                        trans.Rollback();
                         CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
      
                        return 0;
                        //throw;
                    }
                }
            }
        }

        #endregion

    }
}
