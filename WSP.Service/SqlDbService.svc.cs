using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WSP.DBUtility;

namespace WSP.Service
{
    [DataContract]
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DbService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DbService.svc 或 DbService.svc.cs，然后开始调试。

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Single)]
    public class SqlDbService : ISqlDbService
    {
        private ISQLHelper sqlHelper = null;
        //{
        //    get
        //    {
        //        return new SQLHelper("Coal3d");
        //    }
        //}

        public SqlDbService()
        {

        }

        public bool init(string datasource)
        {
            //if (sqlHelper == null) {

            string conn = CFunctions.getConfigName(datasource);
            if (!string.IsNullOrEmpty(conn))
            {
                sqlHelper = new SQLHelper(conn);
                return true;
            }
            else
            {
                return false;
            }

            // }
            // return true;
        }


        public string Test()
        {
            sqlHelper.getConn();
            return "Hello World!";
        }

        /// <summary>
        /// 根据存储过程获取Datatable，第二个参数可以返回改变后的值
        /// </summary>
        /// <param name="ProcName">存储过程名字</param>
        /// <param name="pas">参数集合</param>
        /// <returns></returns>
        public DataTable GetTableByProcRef(string ProcName, ref CmdParameter[] pas)
        {
            //  string x=getIpPort();
            SqlParameter[] sqlPrams = ConvertParameter(pas);
            DataTable dt = sqlHelper.GetTableByProc(ProcName, ref sqlPrams);
            pas = RecoverParameter(sqlPrams);
            return dt;
        }
        /// <summary>
        /// 根据存储过程获取Datatable
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public DataTable GetTableByProc(string ProcName, CmdParameter[] pas)
        {
            SqlParameter[] sqlPrams = ConvertParameter(pas);
            return sqlHelper.GetTableByProc(ProcName, sqlPrams);

        }

        /// <summary>
        /// 根据存储过程获取DataSet
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas)
        {
            //try
            //{
            //   string x=getIpPort();
            SqlParameter[] sqlPrams = ConvertParameter(pas);
            return sqlHelper.GetDataSetByProc(ProcName, sqlPrams);
            //}
            //catch (Exception ex)
            //{

            //    return null;
            //}
        }

        /// <summary>
        /// 执行存储过程，返回受影响的行数，用于增删改
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExcuteProc(string ProcName, CmdParameter[] pars)
        {
            // return 1;
            SqlParameter[] sqlPrams = ConvertParameter(pars);
            return sqlHelper.ExcuteProc(ProcName, sqlPrams);
        }


        /// <summary>
        /// 执行存储过程，返回第一行第一列的值，用于查询
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExcuteScalarProc(string ProcName, CmdParameter[] pars)
        {
            // return 1;
            SqlParameter[] sqlPrams = ConvertParameter(pars);
            return sqlHelper.ExcuteScalarProc(ProcName, sqlPrams);
        }


        /// <summary>
        /// 将自定义的CmdParameter类型转换为SqlParameter
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        private SqlParameter[] ConvertParameter(CmdParameter[] prams)
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
                    sqlPrams[i].Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), prams[i].Direction.ToString(), true);// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }


        /// <summary>
        /// 将SqlParameter类型转换为CmdParameter
        /// </summary>
        /// <param name="prams"></param>
        /// <returns></returns>
        private CmdParameter[] RecoverParameter(SqlParameter[] prams)
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
                    //sqlPrams[i].Direction =  prams[i]. .Direction;// ParameterDirection.Output;
                }
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return sqlPrams;
        }

        /// <summary>
        /// 创建CmdParameter类型的带参构造函数，因为WCF不支持带参数的构造函数，所以放在这里创建
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public CmdParameter CmdParameter(string paraName, object Value)
        {
            CmdParameter cmp = new CmdParameter();
            try
            {
                cmp.ParameterName = paraName;
                cmp.Value = Value;
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return cmp;
        }



        /// <summary>
        /// 获取客户端调用的IP和端口，暂时放置
        /// </summary>
        /// <returns></returns>

    }
}