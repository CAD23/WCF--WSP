using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WSP.Service
{
    public partial class WebConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
                Response.End();
            }


            string action = Context.Request["action"];
            switch (action)
            {
                case "getData":
                    readXmlData(this.Context); break;
                case "saveData":
                    saveXmlData(this.Context); break;
            }


        }

        //保存配置文件信息
        private void saveXmlData(HttpContext context)
        {
            try
            {
                Context.Response.ContentType = "text/plain";
                string content = Context.Request["content"];
                int i = 0;
                //  string content = Request.QueryString["content"];

                if (!string.IsNullOrEmpty(content))
                {
                    //string s = Request.QueryString["name"];
                    

                    List<ContentModel> list = JsonConvert.DeserializeObject<List<ContentModel>>(content);

                    string type = Context.Request["type"];
                    string path = System.AppDomain.CurrentDomain.BaseDirectory;
                    if (type == "1")
                    {
                        path += "/DataBaseConfig/ComSource.xml";
                    }
                    else if (type == "2")
                    {
                        path += "/DataBaseConfig/OraDataSource.xml";
                    }
                    else if (type == "3")
                    {
                        path += "/DataBaseConfig/SqlDataSource.xml";
                    }
                    else if (type == "4")
                    {
                        path += "/DataBaseConfig/MySqlDataSource.xml";
                    }
                    else
                    {
                        return;
                    }


                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode xn = doc.SelectSingleNode("DataSource");
                    if (xn != null)
                    {
                        xn.RemoveAll();
                        foreach (ContentModel model in list)
                        {
                            XmlElement xe1 = doc.CreateElement("item");//创建一个<item>节点  
                            xe1.SetAttribute("name", model.name);//设置该节点name属性  
                            xe1.SetAttribute("value", model.value);//设置该节点value属性  
                            xe1.SetAttribute("dbname", model.dbname);
                            xe1.SetAttribute("text", model.text);
                            xe1.SetAttribute("enable", model.enable);
                            xe1.SetAttribute("canget", model.canget);
                            xn.AppendChild(xe1);
                        }
                    }
                    doc.Save(path);
                    Response.Write("保存成功!");
                    Response.End(); //这个必须
                }
            }
            catch (Exception ex)
            {
                WSP.DBUtility.CFunctions.WriteLog(ex.ToString());
            }

        }
        //读取配置文件里的信息
        private void readXmlData(HttpContext context)
        {
         

            string type = Context.Request["type"];
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            if (type == "1")
            {
                path += "/DataBaseConfig/ComSource.xml";
            }
            else if (type == "2")
            {
                path += "/DataBaseConfig/OraDataSource.xml";
            }
            else if (type == "3")
            {
                path += "/DataBaseConfig/SqlDataSource.xml";
            }
            else if (type == "4")
            {
                path += "/DataBaseConfig/MySqlDataSource.xml";
            }
            else
            {
                return;
            }

            StringBuilder sdata = new StringBuilder();
            sdata.Append("[");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode xn = doc.SelectSingleNode("DataSource");
            if (xn != null)
            {
                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode xn1 in xnl)
                {

                    if (xn1 is XmlElement)
                    {
                        sdata.Append("{");
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
                        int len = xe.Attributes.Count;

                        for (int i = 0; i < len; i++)
                        {
                            if (i == len - 1)
                            {
                                sdata.Append("'" + xe.Attributes[i].Name + "':" + "'" + xe.Attributes[i].Value + "'");
                            }
                            else
                            {
                                sdata.Append("'" + xe.Attributes[i].Name + "':" + "'" + xe.Attributes[i].Value + "',");
                            }

                        }

                        if (xn1.NextSibling == null)
                        {
                            sdata.Append("}");
                        }
                        else
                        {
                            sdata.Append("},");
                        }
                    }
                }
            }
            sdata.Append("]");

            Context.Response.ContentType = "text/plain";

            string json = JsonConvert.SerializeObject(sdata.ToString());

            Response.Write(json);
            Response.End(); //这个必须
        }

    }

    [Serializable]
    [DataContract]//得在定义对象类前，先声明这两个属性
    public class ContentModel
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("dbname")]
        public string dbname { get; set; }

        [JsonProperty("text")]
        public string text { get; set; }

        [JsonProperty("enable")]
        public string enable { get; set; }

        [JsonProperty("canget")]
        public string canget { get; set; }


    }
}