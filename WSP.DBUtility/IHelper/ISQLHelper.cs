using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WSP.DBUtility
{
    public interface ISQLHelper
    {

        /// <summary>
        ///  -------------------获取连接字符串
        /// </summary>
        /// <returns></returns>
         string getConn();

        /// <summary>
        /// 返回一个Datatable
        /// </summary>
        /// <param name="strSQL">查询SQL</param>
        /// <returns></returns>
         DataTable GetTable(string strSQL);

        /// <summary>
        /// 返回DataTable，查询语句带参数
        /// </summary>
        /// <param name="strSQL">查询SQL</param>
        /// <param name="pas">参数集合</param>
        /// <returns></returns>
         DataTable GetTable(string strSQL, SqlParameter[] pas);

         DataTable GetTableByProc(string ProcName, ref SqlParameter[] pas);

         DataTable GetTableByProc(string ProcName, SqlParameter[] pas);

         DataSet GetDataSetByProc(string ProcName, SqlParameter[] pas);
        
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
         DataSet GetDataSet(string strSQL);

        /// <summary>
         /// 返回一个DataSet,查询SQL带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
         DataSet GetDataSet(string strSQL, SqlParameter[] pas);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
          int ExcuteSQL(string strSQL);

        /// <summary>
        /// 执行SQL语句，带参数
        /// </summary>
        /// <param name="strSQL">要执行的SQL</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
          int ExcuteSQL(string strSQL, SqlParameter[] paras);


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
          int ExcuteProc(string ProcName, SqlParameter[] pars);

        /// <summary>
          /// 执行存储过程，带参数,用于查询，返回第一行第一列的值
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
          int ExcuteScalarProc(string ProcName, SqlParameter[] pars);
        

        /// <summary>
        /// 返回第一行第一列，常用于查询数据行数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
          int ExcuteScalarSQL(string strSQL);

        /// <summary>
        /// 返回第一行第一列，常用于查询数据行数,可带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
          int ExcuteScalarSQL(string strSQL, SqlParameter[] paras);


        /// <summary>
        /// 获取第一行的值
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
          object GetObject(string strSQL);

        /// <summary>
        /// 获取第一行的值，SQL带参数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
          object GetObject(string strSQL, SqlParameter[] paras);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        void  ExecuteSqlTran(Hashtable SQLStringList);
    }
}
