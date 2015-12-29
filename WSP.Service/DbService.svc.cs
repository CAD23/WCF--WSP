using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WSP.DBUtility;
using WSP.DBUtility.Helper;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DbService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DbService.svc 或 DbService.svc.cs，然后开始调试。

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Single)]
    public class DbService : IDbService
    {
        private IDbHelper sqlHelper = null;//统一数据处理接口
       
        public DbService()
        {

        }
        
        /// <summary>
        /// 初始化方法，根据itemname和dbtype获取数据库连接字符串，并决定初始化哪一种数据处理helper
        /// </summary>
        /// <param name="itemname">节点的名称</param>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public bool init(string itemname, string dbtype)
        {
            //if (sqlHelper == null) {

            string conn = CFunctions.getConfigName(itemname, dbtype);

            if (dbtype.ToUpper().Contains("SQLSERVER"))
            {
                sqlHelper = new SqlDbHelper(conn);
                return true;
            }
            else if (dbtype.ToUpper().Contains("ORACLE"))
            {
                sqlHelper = new OraDbHelper(conn);
                return true;
            }
            else if (dbtype.ToUpper().Contains("MYSQL"))
            {
                sqlHelper = new MySqlDbHelper(conn);
                return true;
            }
            return false;



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
            DataTable dt = sqlHelper.GetTableByProc(ProcName, ref pas);
            // pas = CFunctions.RecoverParameter(pas);
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
            return sqlHelper.GetTableByProc(ProcName, pas);

        }

        /// <summary>
        /// 根据存储过程获取DataSet
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas)
        {
            ;
            return sqlHelper.GetDataSetByProc(ProcName, pas);

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
            return sqlHelper.ExcuteProc(ProcName, pars);
        }

        /// <summary>
        /// 返回一个CmdParameter类型的对象
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="Value"></param>
        /// <param name="paramtype"></param>
        /// <param name="dbtype"></param>
        /// <param name="size"></param>
        /// <param name="paramDirection"></param>
        /// <returns></returns>
        public CmdParameter CmdParameter(string paraName, object Value, string paramtype, string dbtype, int size, ParameterDirection paramDirection)
        {

            try
            {
                CmdParameter cmp = new CmdParameter(paraName, Value, paramtype, dbtype, size, paramDirection);
                return cmp;
            }
            catch (Exception ex)
            {
                CFunctions.HandleException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            return null;
        }

    }
}
