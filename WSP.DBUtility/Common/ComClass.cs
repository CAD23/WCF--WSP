using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WSP.DBUtility
{
    class ComClass
    {
    }


    [Serializable]
    [DataContract]
    public class CmdParameter
    {
        public CmdParameter()
        {

        }
        /// <summary>
        /// 全参构造函数
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="Value"></param>
        /// <param name="patamtype"></param>
        /// <param name="dbtype"></param>
        /// <param name="size"></param>
        /// <param name="paramDirection"></param>
        public CmdParameter(string ParameterName, object Value, string patamtype, string dbtype, int size, ParameterDirection paramDirection)
        {
           
            this.Value = Value;
            if (dbtype.ToUpper().Contains("SQLSERVER"))
            {
                this.sql_Type = getSqlDbType(patamtype);
                this.ParameterName ="@"+ ParameterName;
            }
            else if (dbtype.ToUpper().Contains("ORACLE"))
            {
                this.oracle_type = getOraDbType(patamtype);
                this.ParameterName = ParameterName;
            }
            else if (dbtype.ToUpper().Contains("MYSQL"))
            {
                this.mySql_Type = getMySqlDbType(patamtype);
                this.ParameterName = ParameterName;
           
            }
            this.size = size;
            this.paramDirection = paramDirection;
        }

        public CmdParameter(string ParameterName, object Value, string patamtype, string dbtype)
        {

            this.Value = Value;
            if (dbtype.ToUpper().Contains("SQLSERVER"))
            {
                this.sql_Type = getSqlDbType(patamtype);
                this.ParameterName = "@" + ParameterName;
            }
            else if (dbtype.ToUpper().Contains("ORACLE"))
            {
                this.oracle_type = getOraDbType(patamtype);
                this.ParameterName = ParameterName;
            }
            else if (dbtype.ToUpper().Contains("MYSQL"))
            {
                this.mySql_Type = getMySqlDbType(patamtype);
                this.ParameterName = ParameterName;

            }
        }
        //Input = 1, Output = 2,InputOutput = 3, ReturnValue = 6,
        [DataMember]
        public int Direction;

        [DataMember]
        public string ParameterName;

        [DataMember]
        public object Value;

         [DataMember]
        public ParameterDirection paramDirection = ParameterDirection.Input;

        [DataMember]
         public OracleType oracle_type = OracleType.VarChar;

        [DataMember]
        public SqlDbType sql_Type = SqlDbType.VarChar;

        [DataMember]
        public MySqlDbType mySql_Type = MySqlDbType.VarChar;

        [DataMember]
        public int size=0;

        public CmdParameter(string ParameterName, object Value)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;

        }

       

        private SqlDbType getSqlDbType(string name)
        {
            switch (name.ToUpper())
            {
                case "VARCHAR":
                    return SqlDbType.VarChar;
                case "INT":
                    return SqlDbType.Int;
                case "DATETIME":
                    return SqlDbType.DateTime;
                case "DATE":
                    return SqlDbType.Date;
                case "BINARY":
                    return SqlDbType.Binary;
                case "IMAGE":
                    return SqlDbType.Image;
                case "TEXT":
                    return SqlDbType.Text;
                case "FLOAT":
                    return SqlDbType.Float;
                case "DECIMAL":
                    return SqlDbType.Decimal;
                case "BOOL":
                    return SqlDbType.Bit;
                default:
                    return SqlDbType.VarChar;

            }
        }
        private OracleType getOraDbType(string name)
        {
            switch (name.ToUpper())
            {
                case "VARCHAR":
                    return OracleType.VarChar;
                case "CURSOR":
                    return OracleType.Cursor;
                case "INT":
                    return OracleType.Int32;
                case "NUMBER":
                    return OracleType.Number;
                case "DATETIME":
                    return OracleType.DateTime;
                case "FLOAT":
                    return OracleType.Float;
                case "DOUBLE":
                    return OracleType.Double;
                case "BLOB":
                    return OracleType.Blob;
                case "CLOB":
                    return OracleType.Clob;
                default:
                    return OracleType.VarChar;

            }
        }

        private MySqlDbType getMySqlDbType(string name)
        {
            switch (name.ToUpper())
            {
                case "VARCHAR":
                    return MySqlDbType.VarChar;
                case "INT":
                    return MySqlDbType.Int32;
                case "DECIMAL":
                    return MySqlDbType.Decimal;
                case "DATETIME":
                    return MySqlDbType.DateTime;
                case "FLOAT":
                    return MySqlDbType.Float;
                case "DOUBLE":
                    return MySqlDbType.Double;
                case "BLOB":
                    return MySqlDbType.Blob ;
                case "DATE":
                    return MySqlDbType.Date;
                case "BINARY":
                    return MySqlDbType.Binary ;
                case "BYTE":
                    return MySqlDbType.Byte ;
                case "VARBINARY":
                    return MySqlDbType.VarBinary;
                default:
                    return MySqlDbType.VarChar;

            }
        }
    }
}
