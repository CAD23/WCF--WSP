using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSP.Service
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string username = Context.Request["username"];
                string password = Context.Request["password"];

               string uname= System.Configuration.ConfigurationManager.ConnectionStrings["username"].ConnectionString;
               string pwd = System.Configuration.ConfigurationManager.ConnectionStrings["password"].ConnectionString;

               if (username == uname && password == pwd)
               {
                   this.label.InnerText = "登录成功!";
                   Session["user"] = username;
                   Response.Redirect("WebConfig.aspx");
                   Response.End();
               }
               else {
                   this.label.InnerText = "用户名或者密码错误!";
               }
            }
          

        }
    }
}