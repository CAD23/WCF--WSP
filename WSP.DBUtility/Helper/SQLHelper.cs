using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ServiceModel;

namespace WSP.DBUtility
{
    public class SQLHelper : ISQLHelper
    {
        //�����ַ���
        private  string strConn = "";

        public SQLHelper()
        { 
            
        }
        public SQLHelper(string conn)
        {
            //strConn = ConfigurationManager.ConnectionStrings[dbname].ToString();
            strConn = conn;
        }

      

        #region 1-------------------��ȡ�����ַ���
        public string getConn()
        {
            return "";
        }
        #endregion


        #region ִ�в�ѯ������DataTable����-----------------------



        public DataTable GetTable(string strSQL)
        {
            return GetTable(strSQL, null);
        }
        public DataTable GetTable(string strSQL, SqlParameter[] pas)
        {
            return GetTable(strSQL, pas, CommandType.Text);
        }
        /// <summary>
        /// ִ�в�ѯ������DataTable����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <param name="pas">��������</param>
        /// <param name="cmdtype">Command����</param>
        /// <returns>DataTable����</returns>
        public  DataTable GetTable(string strSQL, SqlParameter[] pas, CommandType cmdtype)
        {
            DataTable dt = new DataTable(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
                    try
                    {
                        da.SelectCommand.CommandType = cmdtype;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        da.Fill(dt);
                        dt.TableName = "dtname";
                    }
                    catch (Exception ex) {
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                        throw (ex);
                    }

            }
            return dt;
        }


        public DataTable GetTableByProc(string ProcName, ref SqlParameter[] pas)
        {
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
                        dt.TableName = ProcName;
                        da.SelectCommand.Parameters.Clear();
                    }
                    catch (Exception ex) {
                         CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                         throw (ex);
                    }
                try {
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

                    for (int i = 0; i < pas.Length; i++) {
                        pas[i] = cmd.Parameters[i];
                    }
   
                }
                catch (Exception ex) { }
            }
            return dt;
        }


        public DataTable GetTableByProc(string ProcName, SqlParameter[] pas)
        {
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
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw (ex);
                    }
               
            }
            return dt;
        }
        #endregion




        #region ִ�в�ѯ������DataSet����-------------------------




        public DataSet GetDataSet(string strSQL)
        {
            return GetDataSet(strSQL, null);
        }

        public DataSet GetDataSet(string strSQL, SqlParameter[] pas)
        {
            return GetDataSet(strSQL, pas, CommandType.Text);
        }
        /// <summary>
        /// ִ�в�ѯ������DataSet����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <param name="pas">��������</param>
        /// <param name="cmdtype">Command����</param>
        /// <returns>DataSet����</returns>
        public DataSet GetDataSet(string strSQL, SqlParameter[] pas, CommandType cmdtype)
        {
            DataSet dt = new DataSet(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
                    try
                    {
                        da.SelectCommand.CommandType = cmdtype;
                        if (pas != null)
                        {
                            da.SelectCommand.Parameters.AddRange(pas);
                        }
                        da.Fill(dt);
                       
                        //command.Parameters["ReturnValue"].Value;
                    }
                    catch (Exception ex) {
                         CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                         throw (ex);
                    }

            }
            return dt;
        }

        public DataSet GetDataSetByProc(string strSQL, SqlParameter[] pas)
        {
            DataSet dt = new DataSet(); ;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSQL, conn))
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
                        CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw (ex);
                    }

            }
            return dt;
        }
        #endregion





        #region ִ�зǲ�ѯ�洢���̺�SQL���-----------------------------




        public int ExcuteProc(string ProcName)
        {
            return ExcuteSQL(ProcName, null, CommandType.StoredProcedure);
        }

        public int ExcuteProc(string ProcName, SqlParameter[] pars)
        {
            return ExcuteSQL(ProcName, pars, CommandType.StoredProcedure);
        }

        public int ExcuteSQL(string strSQL)
        {
            return ExcuteSQL(strSQL, null);
        }

        public int ExcuteSQL(string strSQL, SqlParameter[] paras)
        {
            return ExcuteSQL(strSQL, paras, CommandType.Text);
        }

        /// <summary>
        /// ִ�зǲ�ѯ�洢���̺�SQL���
        /// ����ɾ����
        /// </summary>
        /// <param name="strSQL">Ҫִ�е�SQL���</param>
        /// <param name="paras">�����б�û�в�������null</param>
        /// <param name="cmdType">Command����</param>
        /// <returns>����Ӱ������</returns>
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
                             CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                           
                            trans.Rollback();
                            throw (ex);
                        }

                }
            }
            return i;

        }


        #endregion








        #region ִ�в�ѯ���ص�һ�У���һ��---------------------------------




        public int ExcuteScalarSQL(string strSQL)
        {
            return ExcuteScalarSQL(strSQL, null);
        }

        public int ExcuteScalarSQL(string strSQL, SqlParameter[] paras)
        {
            return ExcuteScalarSQL(strSQL, paras, CommandType.Text);
        }
        public int ExcuteScalarProc(string strSQL, SqlParameter[] paras)
        {
            return ExcuteScalarSQL(strSQL, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// ִ��SQL��䣬���ص�һ�У���һ��
        /// </summary>
        /// <param name="strSQL">Ҫִ�е�SQL���</param>
        /// <param name="paras">�����б�û�в�������null</param>
        /// <returns>����Ӱ������</returns>
        public int ExcuteScalarSQL(string strSQL, SqlParameter[] paras, CommandType cmdType)
        {
            int i = 0;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    try
                    {
                        cmd.CommandType = cmdType;
                        if (paras != null)
                        {
                            cmd.Parameters.AddRange(paras);
                        }
                        conn.Open();
                        i = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                         CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);  throw (ex);
                    }
                }


            }
            return i;

        }


        #endregion









        #region ��ѯ��ȡ����ֵ------------------------------------




        /// <summary>
        /// ���ò��������Ĵ洢���̻�ȡ����ֵ
        /// </summary>
        /// <param name="ProcName"></param>
        /// <returns></returns>
        public object GetObjectByProc(string ProcName)
        {
            return GetObjectByProc(ProcName, null);
        }
        /// <summary>
        /// ���ô������Ĵ洢���̻�ȡ����ֵ
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public object GetObjectByProc(string ProcName, SqlParameter[] paras)
        {
            return GetObject(ProcName, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// ����sql����ȡ����ֵ
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public object GetObject(string strSQL)
        {
            return GetObject(strSQL, null);
        }
        /// <summary>
        /// ����sql��� �� ���������ȡ����ֵ
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public object GetObject(string strSQL, SqlParameter[] paras)
        {
            return GetObject(strSQL, paras, CommandType.Text);
        }

        /// <summary>
        /// ִ��SQL��䣬������������
        /// </summary>
        /// <param name="strSQL">Ҫִ�е�SQL���</param>
        /// <param name="paras">�����б�û�в�������null</param>
        /// <returns>���ص���������</returns>
        public object GetObject(string strSQL, SqlParameter[] paras, CommandType cmdtype)
        {
            object o = null;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                    try
                    {
                        cmd.CommandType = cmdtype;
                        if (paras != null)
                        {
                            cmd.Parameters.AddRange(paras);

                        }

                        conn.Open();
                        o = cmd.ExecuteScalar();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                         CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                         throw (ex);
                    }

            }
            return o;

        }



        #endregion





        #region ��ѯ��ȡDataReader------------------------------------




        /// <summary>
        /// ���ò��������Ĵ洢���̣�����DataReader����
        /// </summary>
        /// <param name="procName">�洢��������</param>
        /// <returns>DataReader����</returns>
        public  SqlDataReader GetReaderByProc(string procName)
        {
            return GetReaderByProc(procName, null);
        }
        /// <summary>
        /// ���ô��в����Ĵ洢���̣�����DataReader����
        /// </summary>
        /// <param name="procName">�洢������</param>
        /// <param name="paras">��������</param>
        /// <returns>DataReader����</returns>
        public  SqlDataReader GetReaderByProc(string procName, SqlParameter[] paras)
        {
            return GetReader(procName, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// ����sql��䷵��DataReader����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <returns>DataReader����</returns>
        public  SqlDataReader GetReader(string strSQL)
        {
            return GetReader(strSQL, null);
        }
        /// <summary>
        /// ����sql���Ͳ�������DataReader����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <param name="paras">��������</param>
        /// <returns>DataReader����</returns>
        public  SqlDataReader GetReader(string strSQL, SqlParameter[] paras)
        {
            return GetReader(strSQL, paras, CommandType.Text);
        }
        /// <summary>
        /// ��ѯSQL����ȡDataReader
        /// </summary>
        /// <param name="strSQL">��ѯ��SQL���</param>
        /// <param name="paras">�����б�û�в�������null</param>
        /// <returns>��ѯ����DataReader���رոö����ʱ���Զ��ر����ӣ�</returns>
        public  SqlDataReader GetReader(string strSQL, SqlParameter[] paras, CommandType cmdtype)
        {
            SqlDataReader sqldr = null;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = cmdtype;
            if (paras != null)
            {
                cmd.Parameters.AddRange(paras);
            }
            conn.Open();
            //CommandBehavior.CloseConnection�����������������DataReader����رգ��������Զ��ر�
            sqldr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sqldr;
        }



        #endregion




        #region ������������---------------------------------------------




        /// <summary>
        /// �����ݿ���������������
        /// </summary>
        /// <param name="sourceDt">����Դ��</param>
        /// <param name="targetTable">��������Ŀ���</param>
        public  void BulkToDB(DataTable sourceDt, string targetTable)
        {
            SqlConnection conn = new SqlConnection(strConn);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);   //������Դ��������Ч��������sql server����
            bulkCopy.DestinationTableName = targetTable;    //��������Ŀ��������
            bulkCopy.BatchSize = sourceDt.Rows.Count;   //ÿһ�����е�����

            try
            {
                conn.Open();
                if (sourceDt != null && sourceDt.Rows.Count != 0)
                    bulkCopy.WriteToServer(sourceDt);   //���ṩ������Դ�е������и��Ƶ�Ŀ�����
            }
            catch (Exception ex)
            {
                 CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name,ex);
                throw ex;
            }
            finally
            {
                conn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }

        }

        #endregion



        #region -----------------������Ĵ���
        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //ѭ��
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        private  void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion


        
    }
}