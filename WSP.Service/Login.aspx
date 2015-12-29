<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WSP.Service.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body style="text-align: center;">
    <form runat="server" action="Login.aspx" method="post">

        <div style="width: 280px;height:30px;margin: auto; margin-top: 200px;text-align:right">
            <a href="ServicesList.aspx">服务查看页面</a>
        </div>
        <div style="width: 280px; height: 150px; margin: auto; border: 1px solid">
            <table>
                <tr style="height: 35px;">
                    <td style="text-align: right">用户名：
                    </td>
                    <td>
                        <input type="text" id="username" style="height: 20px; width: 150px;"  runat="server" />
                    </td>
                </tr>
                <tr style="height: 35px;">
                    <td style="text-align: right">密码：
                    </td>
                    <td>
                        <input type="password" id="password" style="height: 20px; width: 150px;" runat="server" />
                    </td>
                </tr>

                <tr style="height: 45px;">
                    
                    <td colspan="2" style="text-align: right; margin-top: 10px;">
                        <input type="submit" value="登录" style="width: 80px; height: 25px;" />
                    </td>
                </tr>

                <tr style="height: 20px;font-size:12px;color:red;">
                        <td colspan="2" style="text-align: center">
                         <label id="label" runat="server"></label>
                    </td>
                </tr>
            </table>


        </div>

    </form>


</body>
</html>
