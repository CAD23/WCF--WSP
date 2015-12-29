using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace WSP.Service
{
    public class ComHelper
    {
        private static string getIpPort(string type)
        {
            //提供方法执行的上下文环境  
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性  
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口  
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (type == "0")//返回IP
            {
                return endpoint.Address ;
            }
            else if (type == "1")//返回端口
            {
                return endpoint.Port.ToString();
            }else{//返回IP和端口
                return endpoint.Address + ":" + endpoint.Port;
            }
            
        }

        /// <summary>
        /// 获取客户端的IP
        /// </summary>
        /// <returns></returns>
        public static string getIp()
        {
            return getIpPort("0");
        }

        /// <summary>
        /// 获取客户端的端口
        /// </summary>
        /// <returns></returns>
        public static string getPort()
        {
            return getIpPort("1");
        }

        /// <summary>
        /// 获取客户端的端口和IP
        /// </summary>
        /// <returns></returns>
        public static string getIpAndPort()
        {
            return getIpPort("2");
        }


    }
}