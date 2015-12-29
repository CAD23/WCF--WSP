using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ComService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ComService.svc 或 ComService.svc.cs，然后开始调试。
    public class ComService : IComService
    {

        //测试函数
        public void DoWork()
        {
        }


        //用来获取Comsource.xml中配置的公用信息
        public string getComConValue(string name)
        {
            string value = WSP.DBUtility.CFunctions.getComConfigValue(name);

            return value;
        }

        //获取配置信息的value
        public string getConValue(string dbtype, string itemname)
        {
            //WSP.DBUtility.CFunctions.WriteLog("这时候访问了");
            string value = WSP.DBUtility.CFunctions.getConfigName(dbtype, itemname, "value", false);

            return value;
        }

        //根据传过来的数据库类型，去对应的配置文件中获取对应节点的信息
        public string getConDbname(string dbtype,string itemname) 
        {
           // WSP.DBUtility.CFunctions.WriteLog("这时候访问了");
            string value = WSP.DBUtility.CFunctions.getConfigName(dbtype, itemname, "dbname",true);
            return value;
        }
        public Dictionary<string, Object> getDbItems(string dbtype)
        {
            Dictionary<string, Object> list = WSP.DBUtility.CFunctions.getConfigItems(dbtype);
            return list;
        }


    }
}
