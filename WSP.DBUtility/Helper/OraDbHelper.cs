using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace WSP.DBUtility
{
    public class OraDbHelper : IDbHelper
    {
        //连接字符串
        private  string strConn = "";

        #region 0----------------------构造函数，包含初始化数据库连接字符串

        public OraDbHelper()
        {
            // strConn = ConfigurationManager.AppSettings["BIM"];    
        }
        public OraDbHelper(string conn)
        {
            //strConn = ConfigurationManager.ConnectionStrings[dbname].ToString();
            strConn = conn;
        }

        #endregion


        #region 1-------------------获取连接字符串
        public string getConn()
        {
            return "";
        }
        #endregion


        #region 2-----------------根据存储过程获取DataTable 的方法
        /// <summary>
        /// 根据存储过程获取DataTable，参数的修改会被返回
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataTable GetTableByProc(string ProcName, ref CmdParameter[] paras)
        {
            OracleParameter[] pas = CFunctions.ConvertToOraParameter(paras);
            DataTable dt = new DataTable(); ;
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                using (OracleDataAdapter da = new OracleDataAdapter(ProcName, conn))
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        da.Fill(dt);
                        dt.TableName = ProcName;
                        paras = CFunctions.RecoverParameter(pas);
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw (ex);
                    }

            }
            return dt;
        }

        /// <summary>
        /// 根据存储过程获取DataTable
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataTable GetTableByProc(string ProcName, CmdParameter[] paras)
        {

            OracleParameter[] pas = CFunctions.ConvertToOraParameter(paras);
            DataTable dt = new DataTable(); ;
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                using (OracleDataAdapter da = new OracleDataAdapter(ProcName, conn))
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        //da.SelectCommand.Parameters.Add(new OracleParameter("PSQL",OracleType.VarChar,500));
                        //da.SelectCommand.Parameters["PSQL"].Value = " 1=1 ";
                        //da.SelectCommand.Parameters["PSQL"].Direction = ParameterDirection.Input;

                        //da.SelectCommand.Parameters.Add(new OracleParameter("v_cur", OracleType.Cursor));

                        //da.SelectCommand.Parameters["v_cur"].Direction = ParameterDirection.Output;
                        da.Fill(dt);

                        dt.TableName = ProcName;
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw (ex);
                    }

            }
            return dt;
        }
        #endregion


        #region 3---------------根据存储过程获取DataSet 的方法
        public DataSet GetDataSetByProc(string ProcName, CmdParameter[] paras)
        {
            OracleParameter[] pas = CFunctions.ConvertToOraParameter(paras);
            using (OracleConnection connection = new OracleConnection(strConn))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    
                    connection.Open();
                    OracleDataAdapter sqlDA = new OracleDataAdapter();
                    sqlDA.SelectCommand = BuildQueryCommand(connection, ProcName, pas);
                    sqlDA.Fill(dataSet, ProcName);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw (ex);
                }
                return dataSet;
            }
        }
        #endregion


        #region 4----------------------执行存储过程的方法,带参，不带参
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <returns></returns>
        public int ExcuteProc(string ProcName)
        {
            using (OracleConnection connection = new OracleConnection(strConn))
            {
                int result=0;
                try
                {
                    connection.Open();
                    OracleCommand command = BuildIntCommand(connection, ProcName, null);
                    command.ExecuteNonQuery();
                    result = (int)command.Parameters["ReturnValue"].Value;
                }
                catch (Exception ex)
                {
                    CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
                //Connection.Close();
                return result;
            }

        }

        /// <summary>
        /// 执行存储过程，带参数,用于增删改
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="pars">参数集合</param>
        /// <returns></returns>
        public int ExcuteProc(string ProcName, CmdParameter[] paras)
        {
            OracleParameter[] pars = CFunctions.ConvertToOraParameter(paras);
            //CFunctions.HandleBlobParam( ref pars);
            using (OracleConnection connection = new OracleConnection(strConn))
            {
                int result;
                try
                {
                    connection.Open();

                    OracleTransaction tx = connection.BeginTransaction();

                    OracleCommand command = new OracleCommand(ProcName, connection);
                    command.Transaction = tx;

                    command.CommandType = CommandType.StoredProcedure;
                    foreach (OracleParameter parameter in pars)
                    {
                        if (parameter.OracleType == OracleType.Blob && parameter.Value != null && !string.IsNullOrEmpty(parameter.Value.ToString()))
                        {
                            parameter.Value = getOdb(connection, tx, parameter.Value);
                        }
                        command.Parameters.Add(parameter);
                    }
                    //添加一个名为ReturnValue的Output参数，用于返回值
                    command.Parameters.Add(new OracleParameter("ReturnValue", OracleType.Int32, 4, ParameterDirection.Output,
                        false, 0, 0, string.Empty, DataRowVersion.Default, null));


                    //command = BuildIntCommand(connection, ProcName, pars);
                    command.ExecuteNonQuery();
                    tx.Commit();
                    result = (int)command.Parameters["ReturnValue"].Value;
                }
                catch (Exception ex)
                {
                    CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
                //Connection.Close();
                return result;
            }
        }

        #region -----------------根据OracleConnection，OracleTransaction，二进制imgByte 获取一个OracleLob对象，用于处理上传大数据的问题
        public OracleLob getOdb(OracleConnection conn, OracleTransaction tx, object imgBytes)
        {
            try
            {
                byte[] imgByte = (byte[])imgBytes;
                OracleCommand cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                //这里是关键，他定义了一个命令对象的t-sql语句，通过dmbs_lob来创建一个临时对象，这个对象的类型为blob，并存放在变量xx中，然后将xx的值付给外传参数tmpblob
                cmd.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                //构造外传参数对象，并加入到命令对象的参数集合中
                cmd.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();


                //构造OracleLob对象，他的值为tmpblob外传参数的值
                OracleLob tempLob = (OracleLob)cmd.Parameters[0].Value;
                //指定tempLob的访问模式，并开始操作二进制数据
                tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
                //将二进制流byte数组集合写入到tmpLob里
                tempLob.Write(imgByte, 0, imgByte.Length);
                tempLob.EndBatch();

                return tempLob;
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }
        #endregion

        #endregion


        #region 6--------------------------------创建OracleCommand
        /// <summary>
        /// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand</returns>
        private  OracleCommand BuildQueryCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        private  void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 创建 OracleCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand 对象实例</returns>
        private  OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OracleParameter("ReturnValue",
                         OracleType.Int32, 4, ParameterDirection.Output,
             false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion


        #region 7------------------------------根据SQL语句获取Datatable
        // <summary>
        /// 执行查询语句，返回DataTable ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        public DataTable getDataBySql(string strSQL)
        {
            DataTable dt = new DataTable(); ;
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                using (OracleDataAdapter da = new OracleDataAdapter(strSQL, conn))
                    try
                    {
                        da.SelectCommand.CommandType = CommandType.Text;
                        da.Fill(dt);
                        dt.TableName = "tabname";
                    }
                    catch (Exception ex)
                    {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw (ex);
                    }

            }
            return dt;

        }
        #endregion


        #region 8-------------------------------批量执行SQL语句,用于批量导入数据用
        public  int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (string sql in SQLStringList)
                    {
                        if (!String.IsNullOrEmpty(sql))
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return 1;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    tx.Rollback();
                    CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                    return 0;
                    //throw new Exception(E.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    //return 0;
                }
            }
        }
        #endregion
    }
}
