﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.ServiceReference3 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CmdParameter", Namespace="http://schemas.datacontract.org/2004/07/WSP.DBUtility")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.DBNull))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Test.ServiceReference3.CmdParameter[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Data.OracleClient.OracleType))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Data.ParameterDirection))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Data.SqlDbType))]
    public partial class CmdParameter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DirectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParameterNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Data.OracleClient.OracleType oracle_typeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Data.ParameterDirection paramDirectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int sizeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Data.SqlDbType sql_TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Direction {
            get {
                return this.DirectionField;
            }
            set {
                if ((this.DirectionField.Equals(value) != true)) {
                    this.DirectionField = value;
                    this.RaisePropertyChanged("Direction");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterName {
            get {
                return this.ParameterNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterNameField, value) != true)) {
                    this.ParameterNameField = value;
                    this.RaisePropertyChanged("ParameterName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.OracleClient.OracleType oracle_type {
            get {
                return this.oracle_typeField;
            }
            set {
                if ((this.oracle_typeField.Equals(value) != true)) {
                    this.oracle_typeField = value;
                    this.RaisePropertyChanged("oracle_type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.ParameterDirection paramDirection {
            get {
                return this.paramDirectionField;
            }
            set {
                if ((this.paramDirectionField.Equals(value) != true)) {
                    this.paramDirectionField = value;
                    this.RaisePropertyChanged("paramDirection");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int size {
            get {
                return this.sizeField;
            }
            set {
                if ((this.sizeField.Equals(value) != true)) {
                    this.sizeField = value;
                    this.RaisePropertyChanged("size");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.SqlDbType sql_Type {
            get {
                return this.sql_TypeField;
            }
            set {
                if ((this.sql_TypeField.Equals(value) != true)) {
                    this.sql_TypeField = value;
                    this.RaisePropertyChanged("sql_Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference3.IDbService")]
    public interface IDbService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/init", ReplyAction="http://tempuri.org/IDbService/initResponse")]
        bool init(string datasource, string dbtype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/init", ReplyAction="http://tempuri.org/IDbService/initResponse")]
        System.Threading.Tasks.Task<bool> initAsync(string datasource, string dbtype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetTableByProc", ReplyAction="http://tempuri.org/IDbService/GetTableByProcResponse")]
        System.Data.DataTable GetTableByProc(string ProcName, Test.ServiceReference3.CmdParameter[] pas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetTableByProc", ReplyAction="http://tempuri.org/IDbService/GetTableByProcResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetTableByProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetTableByProcRef", ReplyAction="http://tempuri.org/IDbService/GetTableByProcRefResponse")]
        Test.ServiceReference3.GetTableByProcRefResponse GetTableByProcRef(Test.ServiceReference3.GetTableByProcRefRequest request);
        
        // CODEGEN: 正在生成消息协定，应为该操作具有多个返回值。
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetTableByProcRef", ReplyAction="http://tempuri.org/IDbService/GetTableByProcRefResponse")]
        System.Threading.Tasks.Task<Test.ServiceReference3.GetTableByProcRefResponse> GetTableByProcRefAsync(Test.ServiceReference3.GetTableByProcRefRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/ExcuteProc", ReplyAction="http://tempuri.org/IDbService/ExcuteProcResponse")]
        int ExcuteProc(string ProcName, Test.ServiceReference3.CmdParameter[] pars);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/ExcuteProc", ReplyAction="http://tempuri.org/IDbService/ExcuteProcResponse")]
        System.Threading.Tasks.Task<int> ExcuteProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pars);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/CmdParameter", ReplyAction="http://tempuri.org/IDbService/CmdParameterResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.DBNull))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Test.ServiceReference3.CmdParameter[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Test.ServiceReference3.CmdParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Data.OracleClient.OracleType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Data.ParameterDirection))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Data.SqlDbType))]
        Test.ServiceReference3.CmdParameter CmdParameter(string paraName, object Value, string paramtype, string dbtype, int size, System.Data.ParameterDirection paramDirection);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/CmdParameter", ReplyAction="http://tempuri.org/IDbService/CmdParameterResponse")]
        System.Threading.Tasks.Task<Test.ServiceReference3.CmdParameter> CmdParameterAsync(string paraName, object Value, string paramtype, string dbtype, int size, System.Data.ParameterDirection paramDirection);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetDataSetByProc", ReplyAction="http://tempuri.org/IDbService/GetDataSetByProcResponse")]
        System.Data.DataSet GetDataSetByProc(string ProcName, Test.ServiceReference3.CmdParameter[] pas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDbService/GetDataSetByProc", ReplyAction="http://tempuri.org/IDbService/GetDataSetByProcResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetDataSetByProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pas);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTableByProcRef", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetTableByProcRefRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string ProcName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public Test.ServiceReference3.CmdParameter[] pas;
        
        public GetTableByProcRefRequest() {
        }
        
        public GetTableByProcRefRequest(string ProcName, Test.ServiceReference3.CmdParameter[] pas) {
            this.ProcName = ProcName;
            this.pas = pas;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTableByProcRefResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetTableByProcRefResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Data.DataTable GetTableByProcRefResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public Test.ServiceReference3.CmdParameter[] pas;
        
        public GetTableByProcRefResponse() {
        }
        
        public GetTableByProcRefResponse(System.Data.DataTable GetTableByProcRefResult, Test.ServiceReference3.CmdParameter[] pas) {
            this.GetTableByProcRefResult = GetTableByProcRefResult;
            this.pas = pas;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDbServiceChannel : Test.ServiceReference3.IDbService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DbServiceClient : System.ServiceModel.ClientBase<Test.ServiceReference3.IDbService>, Test.ServiceReference3.IDbService {
        
        public DbServiceClient() {
        }
        
        public DbServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DbServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DbServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DbServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool init(string datasource, string dbtype) {
            return base.Channel.init(datasource, dbtype);
        }
        
        public System.Threading.Tasks.Task<bool> initAsync(string datasource, string dbtype) {
            return base.Channel.initAsync(datasource, dbtype);
        }
        
        public System.Data.DataTable GetTableByProc(string ProcName, Test.ServiceReference3.CmdParameter[] pas) {
            return base.Channel.GetTableByProc(ProcName, pas);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetTableByProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pas) {
            return base.Channel.GetTableByProcAsync(ProcName, pas);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Test.ServiceReference3.GetTableByProcRefResponse Test.ServiceReference3.IDbService.GetTableByProcRef(Test.ServiceReference3.GetTableByProcRefRequest request) {
            return base.Channel.GetTableByProcRef(request);
        }
        
        public System.Data.DataTable GetTableByProcRef(string ProcName, ref Test.ServiceReference3.CmdParameter[] pas) {
            Test.ServiceReference3.GetTableByProcRefRequest inValue = new Test.ServiceReference3.GetTableByProcRefRequest();
            inValue.ProcName = ProcName;
            inValue.pas = pas;
            Test.ServiceReference3.GetTableByProcRefResponse retVal = ((Test.ServiceReference3.IDbService)(this)).GetTableByProcRef(inValue);
            pas = retVal.pas;
            return retVal.GetTableByProcRefResult;
        }
        
        public System.Threading.Tasks.Task<Test.ServiceReference3.GetTableByProcRefResponse> GetTableByProcRefAsync(Test.ServiceReference3.GetTableByProcRefRequest request) {
            return base.Channel.GetTableByProcRefAsync(request);
        }
        
        public int ExcuteProc(string ProcName, Test.ServiceReference3.CmdParameter[] pars) {
            return base.Channel.ExcuteProc(ProcName, pars);
        }
        
        public System.Threading.Tasks.Task<int> ExcuteProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pars) {
            return base.Channel.ExcuteProcAsync(ProcName, pars);
        }
        
        public Test.ServiceReference3.CmdParameter CmdParameter(string paraName, object Value, string paramtype, string dbtype, int size, System.Data.ParameterDirection paramDirection) {
            return base.Channel.CmdParameter(paraName, Value, paramtype, dbtype, size, paramDirection);
        }
        
        public System.Threading.Tasks.Task<Test.ServiceReference3.CmdParameter> CmdParameterAsync(string paraName, object Value, string paramtype, string dbtype, int size, System.Data.ParameterDirection paramDirection) {
            return base.Channel.CmdParameterAsync(paraName, Value, paramtype, dbtype, size, paramDirection);
        }
        
        public System.Data.DataSet GetDataSetByProc(string ProcName, Test.ServiceReference3.CmdParameter[] pas) {
            return base.Channel.GetDataSetByProc(ProcName, pas);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetDataSetByProcAsync(string ProcName, Test.ServiceReference3.CmdParameter[] pas) {
            return base.Channel.GetDataSetByProcAsync(ProcName, pas);
        }
    }
}
