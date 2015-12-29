using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WSP.DBUtility
{
    public interface   IOracleHelper
    {

        /// <summary>
        ///  -------------------获取连接字符串
        /// </summary>
        /// <returns></returns>
        string getConn();

       



        DataTable GetTableByProc(string ProcName, ref OracleParameter[] pas);

        DataTable GetTableByProc(string ProcName, OracleParameter[] pas);

        DataSet GetDataSetByProc(string ProcName, OracleParameter[] pas);

       

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <returns></returns>
        int ExcuteProc(string ProcName);

        /// <summary>
        /// 执行存储过程，带参数,用于增删改
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="pars">参数集合</param>
        /// <returns></returns>
        int ExcuteProc(string ProcName, OracleParameter[] pars);

        /// <summary>
        /// 执行存储过程，带参数,用于查询，返回第一行第一列的值
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        //int ExcuteScalarProc(string ProcName, OracleParameter[] pars);


        

      
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
        void ExecuteSqlTran(Hashtable SQLStringList);


        DataTable getDataBySql(string strSQL);
    }
}
