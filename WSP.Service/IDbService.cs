using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WSP.DBUtility;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDbService”。
    [ServiceKnownType(typeof(DBNull))]
    [ServiceContract]
    public interface IDbService
    {
        [OperationContract]
        //初始化连接字符串
        bool init(string datasource,string dbtype);


        [OperationContract]
        /// 根据存储过程获取Datatable
        DataTable GetTableByProc(string ProcName, CmdParameter[] pas);


        [OperationContract]
        /// 根据存储过程获取Datatable,第二个参数可以返回改变后的值
        DataTable GetTableByProcRef(string ProcName, ref CmdParameter[] pas);

        [OperationContract]
        /// 执行存储过程，返回受影响的行数,用于增、删、改，不可用于查询
        int ExcuteProc(string ProcName, CmdParameter[] pars);

        [OperationContract]
        ///创建CmdParameter类型的对象，替代WCF无法传递带参构造函数
        CmdParameter CmdParameter(string paraName, object Value, string paramtype, string dbtype, int size, ParameterDirection paramDirection);

        [OperationContract]
        ///根据存储过程获取DataSet
        DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas);
    }
}
