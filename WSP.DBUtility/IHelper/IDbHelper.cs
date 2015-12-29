using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WSP.DBUtility
{
    public interface IDbHelper
    {
        /// <summary>
        ///  -------------------获取连接字符串
        /// </summary>
        /// <returns></returns>
        string getConn();

        /// <summary>
        /// 根据存储过程查询Datatable数据，查询后的参数会变化返回
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        DataTable GetTableByProc(string ProcName, ref CmdParameter[] pas);


        /// <summary>
        /// 根据存储过程查询Datatable数据，查询后的参数不会变化返回
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        DataTable GetTableByProc(string ProcName, CmdParameter[] pas);

        /// <summary>
        /// 根据存储过程查询DataSet返回
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
         DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas);

      


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
        int ExcuteProc(string ProcName, CmdParameter[] pars);

        /// <summary>
        /// 批量执行SQL语句方法
        /// </summary>
        /// <param name="SQLStringList"></param>
        /// <returns></returns>
        int  ExecuteSqlTran(List<String> SQLStringList);
    }
}
