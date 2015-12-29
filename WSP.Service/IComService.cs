using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSP.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IComService”。
    [ServiceContract]
    public interface IComService
    {
        [OperationContract]
        void DoWork();

        /// <summary>
        /// 获取公共配置文件的信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        string getComConValue(string name);


        /// <summary>
        /// 获取配置文件的信息,需要数据库类型及节点名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        string getConValue(string dbtype, string itemname);


        /// <summary>
        /// 获取对应数据库类型的对应节点的信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        string getConDbname(string dbtype, string itemname);

        /// <summary>
        /// 获取对应数据库类型的所有节点的name集合
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, Object> getDbItems(string dbtype);

    }
}
