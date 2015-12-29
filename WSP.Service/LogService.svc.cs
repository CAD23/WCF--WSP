using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WSP.DBUtility;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“LogService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 LogService.svc 或 LogService.svc.cs，然后开始调试。
    public class LogService : ILogService
    {

        public ISQLHelper sqlHelper
        {
            get
            {
                return new SQLHelper();
            }
        }


        /// <summary>
        /// 记录操作日志信息
        /// </summary>
        /// <param name="ModuleName">模块名字</param>
        /// <param name="ACTION">操作名称-查询、新增、修改、删除</param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool Info(string userid, string ModuleName, string ACTION, string Info)
        {
            bool re = false;
            re = WriteLog(LogLevel.Info, userid, ModuleName, ACTION, Info);
            return re;
        }

        /// <summary>
        /// 记录调试日志信息
        /// </summary>
        /// <param name="ModuleName">模块名字</param>
        /// <param name="ACTION">操作名称-查询、新增、修改、删除</param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public bool Debug(string userid, string ModuleName, string ACTION, string Info)
        {
            bool re = false;
            re = WriteLog(LogLevel.Debug, userid, ModuleName, ACTION, Info);

            return re;
        }

        /// <summary>
        /// 记录出错的日志信息
        /// </summary>
        /// <param name="ModuleName">模块名字</param>
        /// <param name="ACTION">操作名称-查询、新增、修改、删除</param>
        /// <param name="Info">记录的信息</param>
        /// <returns></returns>
        public bool Error(string userid,string ModuleName, string ACTION, string Info)
        {
            bool re = false;
            re = WriteLog(LogLevel.Error, userid, ModuleName, ACTION, Info);
            return re;
        }

        private bool WriteLog(LogLevel level, string userid, string ModuleName, string ACTION, string Info)
        {
            bool re = false;
            SqlParameter[] paras = new SqlParameter[] { 
                  new  SqlParameter("@LOG_GUID",System.Guid.NewGuid().ToString().ToUpper()),
                  new  SqlParameter("@USER_GUID",userid),
                  new  SqlParameter("@USER_IP",ComHelper.getIp()), 
                   new  SqlParameter("@MAC","") ,
                  new  SqlParameter("@LOGLEVEL",level) ,
                  new  SqlParameter("@INFO",Info) ,
                  new  SqlParameter("@MODULENAME",ModuleName) ,
                  new  SqlParameter("@ACTION",ACTION) 
                };
            int result = sqlHelper.ExcuteProc("SP_SYS_LOG_ADD", paras);
            return result>0?true:false;
        }
    }
}
