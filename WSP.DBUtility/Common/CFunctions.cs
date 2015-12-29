using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace WSP.DBUtility
{
    public class CFunctions
    {

        static string strConn = ConfigurationManager.ConnectionStrings["Log"].ToString();

        #region 1----------------------------------捕获未处理的异常的处理方法
        public static void HandleException(string funcname, Exception ex)
        {

           // string x = System.IO.Directory.GetCurrentDirectory();
            //记录日志
            if (!System.IO.Directory.Exists(strConn))
            {
                System.IO.Directory.CreateDirectory(strConn);
            }
            DateTime now = DateTime.Now;
            string logpath = strConn + string.Format(@"\Log_{0}{1}{2}.txt", now.Year, now.Month, now.Day);
            System.IO.File.AppendAllText(logpath, string.Format("{0}--方法:{1}----{2}", now.ToString("yyyy-MM-dd HH:mm:ss"), funcname, ex.Message));
            System.IO.File.AppendAllText(logpath, "\r\n-------------------------------------------------\r\n");

        }

        #endregion

        #region 2---------------------------------获取配置文件的值

        #region 2.0 -----------------------------获取配置节点的信息，参数：节点名称，数据库类型
        public static string getConfigName(string itemname, string dbtype)
        {
            try
            {
                string value = getConfigName(dbtype, itemname, "value",true);
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        #endregion

        #region 2.1 -----------------------------获取SQLSERVER配置文件中节点的信息,参数：节点名称
        public static string getConfigName(string itemname)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/SqlDataSource.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode("DataSource");
                if (xn != null)
                {
                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xn1 in xnl)
                    {
                        if (xn1 is XmlElement)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;

                            if (xe.GetAttribute("name").ToString() == itemname)
                            {
                                return xe.GetAttribute("value").ToString();
                            }
                        }
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        #endregion

        #region 2.2-------------------------------获取公共配置文件的节点信息，参数：节点名称

        public static string getComConfigValue(string itemname)
        {
            try
            {

                string path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/ComSource.xml";
                string value = getValue(itemname, path);
                return value;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        #endregion

        #region 2.3-------------------------------获取配置文件的节点信息，参数:节点名称，文件路径
        public static string getValue(string itemname, string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode("DataSource");
                if (xn != null)
                {
                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xn1 in xnl)
                    {

                        if (xn1 is XmlElement)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;

                            if (xe.GetAttribute("name").ToString() == itemname)
                            {

                                return xe.GetAttribute("value").ToString();
                            }
                        }
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion


        #region 2.4 -----------------------------获取配置节点的信息，参数：节点名称，数据库类型
        public static string getConfigName(string dbtype, string itemname,string attrname,bool isall)
        {
            try
            {

                string path = "";
                if (dbtype.ToUpper().Contains("SQLSERVER"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/SqlDataSource.xml";
                }
                else if (dbtype.ToUpper().Contains("ORACLE"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/OraDataSource.xml";
                }
                else if (dbtype.ToUpper().Contains("MYSQL"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/MySqlDataSource.xml";
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode("DataSource");
                if (xn != null)
                {
                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xn1 in xnl)
                    {
                        if (xn1 is XmlElement)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;

                            if (xe.GetAttribute("name").ToString() == itemname)
                            {
                                if (isall) {
                                    return xe.GetAttribute(attrname).ToString();
                                }
                                else if (xe.GetAttribute("canget").ToString() == "0")
                                {
                                    return xe.GetAttribute(attrname).ToString();
                                }
                                else {
                                    return "";
                                }
                               
                            }
                        }
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                //WriteLog("读取配置文件报错--"+ex.ToString());
                return "";
            }

        }
        #endregion


        #region 2.5 -----------------------------获取配置节点的信息，参数：节点名称，数据库类型
        public static Dictionary<string, Object> getConfigItems(string dbtype)
        {
            Dictionary<string, Object> list = new Dictionary<string, Object>();
            try
            {                string path = "";
                if (dbtype.ToUpper().Contains("SQLSERVER"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/SqlDataSource.xml";
                }
                else if (dbtype.ToUpper().Contains("ORACLE"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/OraDataSource.xml";
                }
                else if (dbtype.ToUpper().Contains("MYSQL"))
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "/DataBaseConfig/MySqlDataSource.xml";
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode("DataSource");
                if (xn != null)
                {
                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xn1 in xnl)
                    {
                        if (xn1 is XmlElement)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;
                            if (xe.GetAttribute("enable").ToString() == "0")
                            {
                                list.Add(xe.GetAttribute("name").ToString(), xe.GetAttribute("text").ToString());
                            }
                            
                        }
                    }
                    return list;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                //WriteLog("读取配置文件报错--"+ex.ToString());
                return list;
            }

        }
        #endregion
        #endregion

        #region 3------------------------------关于服务端自定义类型CmdParameter 和SqlParameter、OracleParameter 相互转换的方法

        #region  3.1-------------------------------将自定义的CmdParameter类型转为SqlParameter类型
        /// <summary>
        /// 将自定义的CmdParameter类型转为SqlParameter类型
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        public static SqlParameter[] ConvertToSqlParameter(CmdParameter[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            SqlParameter[] sqlPrams = new SqlParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    SqlParameter sqlPram = new SqlParameter(prams[i].ParameterName, prams[i].Value);
                    sqlPrams[i] = sqlPram;
                    sqlPrams[i].SqlDbType = prams[i].sql_Type;
                    sqlPrams[i].Direction = prams[i].paramDirection; ; //;(ParameterDirection)Enum.Parse(typeof(ParameterDirection), prams[i].Direction.ToString(), true);// ParameterDirection.Output;
                   
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion

        #region  3.2-----------------------------------将自定义的CmdParameter类型转为OracleParameter类型
        /// <summary>
        /// 将自定义的CmdParameter类型转为OracleParameter类型
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        public static OracleParameter[] ConvertToOraParameter(CmdParameter[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            OracleParameter[] sqlPrams = new OracleParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    OracleParameter sqlPram = new OracleParameter(prams[i].ParameterName, prams[i].Value);
                    sqlPrams[i] = sqlPram;
                    if (prams[i].size != 0)
                    {
                        sqlPrams[i].Size = prams[i].size;
                    }
                    sqlPrams[i].OracleType = prams[i].oracle_type;
                    sqlPrams[i].Direction = prams[i].paramDirection;
                    
                    // sqlPrams[i].Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), prams[i].Direction.ToString(), true);// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion


        #region  3.3--------------------------------------将SqlParameter类型转换为CmdParameter
        /// <summary>
        /// 将SqlParameter类型转换为CmdParameter
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        private CmdParameter[] RecoverSqlParameter(SqlParameter[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            CmdParameter[] sqlPrams = new CmdParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    CmdParameter sqlPram = new CmdParameter(prams[i].ParameterName, prams[i].Value);
                    sqlPrams[i] = sqlPram;
                    //sqlPrams[i].Direction =  prams[i].Direction.ToString();// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion

        #region 3.4----------------------------------------将OracleParameter类型转换为CmdParameter
        /// <summary>
        /// 将SqlParameter类型转换为CmdParameter
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        private CmdParameter[] RecoverOraParameter(OracleParameter[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            CmdParameter[] sqlPrams = new CmdParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    CmdParameter sqlPram = new CmdParameter(prams[i].ParameterName, prams[i].Value);
                    sqlPrams[i] = sqlPram;
                    //sqlPrams[i].Direction =  prams[i].Direction.ToString();// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion


        #region  3.5---------------------------将OracleParameter/SqlParameter 类型转换为CmdParameter
        public static CmdParameter[] RecoverParameter(object[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            CmdParameter[] sqlPrams = new CmdParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    if (prams[i] is SqlParameter)
                    {
                        CmdParameter sqlPram = new CmdParameter((prams[i] as SqlParameter).ParameterName, (prams[i] as SqlParameter).Value);
                        sqlPrams[i] = sqlPram;
                    }
                    else if (prams[i] is OracleParameter)
                    {
                        CmdParameter sqlPram = new CmdParameter((prams[i] as OracleParameter).ParameterName, (prams[i] as OracleParameter).Value);
                        sqlPrams[i] = sqlPram;
                    }
                    else if (prams[i] is MySqlParameter)
                    {
                        CmdParameter sqlPram = new CmdParameter((prams[i] as MySqlParameter).ParameterName, (prams[i] as MySqlParameter).Value);
                        sqlPrams[i] = sqlPram;
                    }

                    //sqlPrams[i].Direction =  prams[i].Direction.ToString();// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion

        #region 3.6--------------------将自定义的CmdParameter转换为MySqlParameter类型
        public static MySqlParameter[] ConvertToMySqlParameter(CmdParameter[] prams)
        {
            if (prams == null)
            {
                return null;
            }
            MySqlParameter[] sqlPrams = new MySqlParameter[prams.Length];
            try
            {

                for (int i = 0; i < prams.Length; i++)
                {
                    MySqlParameter sqlPram = new MySqlParameter(prams[i].ParameterName, prams[i].Value);
                    sqlPrams[i] = sqlPram;
                    if (prams[i].size != 0)
                    {
                        sqlPrams[i].Size = prams[i].size;
                    }
                    sqlPrams[i].MySqlDbType = prams[i].mySql_Type;
                    sqlPrams[i].Direction = prams[i].paramDirection;
                 }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }
        #endregion


        #endregion

        #region 10---------------------测试写入文件信息
        public static void WriteLog(string message)
        {
            //记录日志
            if (!System.IO.Directory.Exists(strConn))
            {
                System.IO.Directory.CreateDirectory(strConn);
            }
            DateTime now = DateTime.Now;
            string logpath = strConn + string.Format(@"\Log_{0}{1}{2}.txt", now.Year, now.Month, now.Day);
            System.IO.File.AppendAllText(logpath, string.Format("{0}--{1}", now.ToString("yyyy-MM-dd HH:mm:ss"),  message));
            System.IO.File.AppendAllText(logpath, "\r\n-------------------------------------------------\r\n");

        }

        #endregion
    }
}
