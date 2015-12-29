using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using WSP.DBUtility;

namespace WSP.Service
{
    [ServiceKnownType(typeof(DBNull))]
    [ServiceContract]
    public interface ISqlDbService
    {

        [OperationContract]
        //初始化连接字符串
        bool init(string datasource);

        [OperationContract]
        //测试接口是否能正常调用
        string Test();

        [OperationContract]
        /// 根据存储过程获取Datatable
        DataTable GetTableByProc(string ProcName,  CmdParameter[] pas);


        [OperationContract]
        /// 根据存储过程获取Datatable,第二个参数可以返回改变后的值
        DataTable GetTableByProcRef(string ProcName, ref CmdParameter[] pas);

        [OperationContract]
        /// 执行存储过程，返回受影响的行数,用于增、删、改，不可用于查询
        int ExcuteProc(string ProcName, CmdParameter[] pars);

        [OperationContract]
        /// 执行存储过程，返回返回第一行第一列的值,用于查询
        int ExcuteScalarProc(string ProcName, CmdParameter[] pars);

       
        [OperationContract]
        ///创建CmdParameter类型的对象，替代WCF无法传递带参构造函数
        CmdParameter CmdParameter(string paraName, object Value);

        [OperationContract]
        ///根据存储过程获取DataSet
        DataSet GetDataSetByProc(string ProcName, CmdParameter[] pas);
    }


    [Serializable] 
    public class SerSqlParameter
    {
        public SerSqlParameter()
        { 
            
        }
        public SerSqlParameter(SqlParameter sPara)
        {
            this.paraName = sPara.ParameterName;
            this.paraLen = sPara.Size;
            this.paraVal = sPara.Value;
            this.sqlDbType = sPara.SqlDbType;
        }

        public SqlParameter ToSqlParameter()
        {
            SqlParameter para = new SqlParameter(this.paraName, this.sqlDbType, this.paraLen);
            para.Value = this.paraVal;
            return para;
        }

        [DataMember]
        private string paraName = "";
        public string ParaName
        {
            get { return this.paraName; }
            set { this.paraName = value; }

        }

        private int paraLen = 0;
        public int ParaLen
        {

            get { return this.paraLen; }
            set { this.paraLen = value; }
        }

        private object paraVal = null;
        public object ParaVal
        {
            get { return this.paraVal; }
            set { this.paraVal = value; }
        }

        private SqlDbType sqlDbType = SqlDbType.NVarChar;
        public SqlDbType SqlDbType
        {
            get { return this.sqlDbType; }

            set { this.sqlDbType = value; }
        }
    }


   
}