using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ILogService”。
    [ServiceContract]
    public interface ILogService
    {

        [OperationContract]
        ///记录操作日志信息
        bool Info(string userid, string ModuleName, string ACTION, string Info);

        [OperationContract]
        ///记录调试日志信息
        bool Debug(string userid, string ModuleName, string ACTION, string Info);

        [OperationContract]
        ///记录出错的日志信息
        bool Error(string userid,string ModuleName, string ACTION, string Info);
    }

    internal enum LogLevel
    { 
        Info=1,
        Debug=2,
        Error=3
    }
}
