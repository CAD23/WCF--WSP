using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WSP.DBUtility;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IOraDbService”。
    [ServiceKnownType(typeof(DBNull))]
    [ServiceContract]
    public interface IOraDbService
    {
        [OperationContract]
        //初始化连接字符串
        bool init(string datasource);

        [OperationContract]
        //测试接口是否能正常调用
        string Test();

        [OperationContract]
        /// 根据存储过程获取Datatable
        DataTable GetTableByProc(string ProcName, OracleParameter[] pas);


        [OperationContract]
        /// 根据存储过程获取Datatable,第二个参数可以返回改变后的值
        DataTable GetTableByProcRef(string ProcName, ref CmdParameter[] pas);

        [OperationContract]
        /// 执行存储过程，返回受影响的行数,用于增、删、改，不可用于查询
        int ExcuteProc(string ProcName, OracleParameter[] pars);

        //[OperationContract]
        /// 执行存储过程，返回返回第一行第一列的值,用于查询
        //int ExcuteScalarProc(string ProcName, CmdParameter[] pars);


        [OperationContract]
        ///创建CmdParameter类型的对象，替代WCF无法传递带参构造函数
        CmdParameter CmdParameter(string paraName, object Value);

        [OperationContract]
        ///根据存储过程获取DataSet
        DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas);

         [OperationContract]
        DataTable getDataBySql(string strSQL);
    }
}
