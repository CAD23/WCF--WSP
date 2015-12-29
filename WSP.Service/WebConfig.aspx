<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebConfig.aspx.cs" Inherits="WSP.Service.WebConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>中间件配置页面</title>
    <style type="text/css">
        .td_name {
            width: 100px;
        }

        .td_value {
            width: 600px;
            height: 25px;
        }
        .td_text_align {
            text-align:right;
        }

         .td_input_align {
            text-align:left;
        }


        .tr {
            height: 40px;
        }


        body, ul, li {
            margin: 0;
            padding: 0;
        }


        #tab {
            overflow: hidden;
            zoom: 1;
            /*background: lightblue;*/
            border: 1px solid #000;
        }

            #tab li {
                float: left;
                /*color: #fff;*/
                height: 30px;
                cursor: pointer;
                line-height: 30px;
                list-style-type: none;
                padding: 0 20px;
            }

                #tab li.current {
                    /*color: #000;*/
                    background: #ccc;
                }

        #content {
            border: 1px solid #000;
            border-top-width: 0;
        }

            #content ul {
                line-height: 25px;
                display: none;
                margin: 0 30px;
                padding: 10px 0;
            }
    </style>


    <script type="text/javascript">

        function load() {

            var oLi = document.getElementById("tab").getElementsByTagName("li");
            var oUl = document.getElementById("content").getElementsByTagName("ul");

            for (var i = 0; i < oLi.length; i++) {
                oLi[i].index = i;
                oLi[i].onclick = function () {
                    for (var n = 0; n < oLi.length; n++) oLi[n].className = "";
                    this.className = "current";
                    for (var n = 0; n < oUl.length; n++) oUl[n].style.display = "none";
                    oUl[this.index].style.display = "block";
                    loadXmlData(this.index+1);
                }
            }
            loadXmlData(1);
            document.getElementById("txtarea").value = '';
          

           
        }

        function loadXmlData(index)
        {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    
                    var cont = xmlhttp.responseText;
                    var jsonObj = eval('(' + cont + ')');
                    jsonObj = eval('(' + jsonObj + ')');
                  
                    document.getElementById("tab" + index).innerHTML="";
                  //  t.firstChild.removeNode(true);
                  //  alert("拼接");
                    for (var i = 0; i < jsonObj.length; i++) {
                      
                        //  var key = jsonObj[i].name;
                        addCsRow(index,jsonObj[i]);
                    }
                }
            }

            xmlhttp.open("POST", "WebConfig.aspx?action=getData&type=" + index, true);
            xmlhttp.send();
           
        }

        function addCsRow(index, row) {
           
            var newTR = document.getElementById("tab" + index).insertRow(document.getElementById("tab" + index).rows.length);
            newTR.className = "tr";
            var newNameTD = newTR.insertCell(0);
            newNameTD.innerHTML = "name：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(1);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name'  value='" + row.name + "' name='name' />";

            var newNameTD = newTR.insertCell(2);
            newNameTD.innerHTML = "value：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(3);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_value' value='" + row.value + "'  name='value'   />";

            var newNameTD = newTR.insertCell(4);
            newNameTD.innerHTML = "dbname：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(5);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name' value='" + row.dbname + "'  name='dbname'   />";

            var newNameTD = newTR.insertCell(6);
            newNameTD.innerHTML = "text：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(7);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name' value='" + row.text + "'  name='text'   />";

            var newNameTD = newTR.insertCell(8);
            newNameTD.innerHTML = "启用：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(9);
            newNameTD.className = "td_input_align";
            var result;
            if (row.enable == '0') {
                result = "checked='checked'";
            } else {
                result = '';
            }
            newNameTD.innerHTML = "<input  type='checkbox' " + result + " name='enable'   />";


            var newNameTD = newTR.insertCell(10);
            newNameTD.innerHTML = "公开：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(11);
            newNameTD.className = "td_input_align";
            if (row.canget == '0') {
                result = "checked='checked'";
            } else {
                result = '';
            }

            newNameTD.innerHTML = "<input  type='checkbox' " + result + "  name='canget'   />";

            var newNameTD = newTR.insertCell(12);
            newNameTD.className = "td_text_align";
            newNameTD.innerHTML = '<input type="button"  value="删除" onclick="delRow(this)" />';
            
        }

        //保存数据
        function saveData(index) {
            var json = '';
            var table = document.getElementById("tab"+index);
            var rows_count = table.rows.length;
            if (rows_count == 0)
            {
                return;
            }
            var cells_count = tab1.rows[0].cells.length;
            json += '[';
            for (var i = 0; i < rows_count; i++) {
                json += '{';
                for (var j = 0; j < cells_count; j++) {
                    var ipt = table.rows[i].cells[j].getElementsByTagName("INPUT")[0];
                    if (ipt != null && ipt != undefined) {
                        if (j == cells_count - 1) {
                            if (ipt.type == "checkbox") {
                                if (ipt.checked) {
                                    json += '"' + ipt.name + '":"0"';
                                } else {
                                    json += '"' + ipt.name + '":"1"';
                                }
                            } else {
                                json += '"' + ipt.name + '":"' + ipt.value + '"';
                            }
                        } else {

                            if (ipt.type == "checkbox") {
                                if (ipt.checked) {
                                    json += '"' + ipt.name + '":"0",';
                                } else {
                                    json += '"' + ipt.name + '":"1",';
                                }
                            } else {

                                json += '"' + ipt.name + '":"' + ipt.value + '",';
                            }

                        }
                    }
                    json += '';
                }
                if (i == rows_count - 1) {
                    json += '}';
                } else {
                    json += '},';
                }

            }
            json += ']';

            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    alert("保存成功!");
                    // document.getElementById("layer2").innerHTML = xmlhttp.responseText;
                }
            }

            var params = "action=saveData&type=" + index + "&content=" + encodeURIComponent(json);
            xmlhttp.open("POST", "WebConfig.aspx", true);
            xmlhttp.setRequestHeader("Content-length", params.length);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded; charset=utf-8"); //注意编码设置
            xmlhttp.send(params);//在这里发送数据

            // xmlhttp.send(null);
        }


        //添加一行
        function addRow(index) {
            var newTR = document.getElementById("tab" + index).insertRow(document.getElementById("tab" + index).rows.length);
            newTR.className = "tr";
            var newNameTD = newTR.insertCell(0);
            newNameTD.innerHTML = "节点名称：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(1);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name'  value='' name='name' />";

            var newNameTD = newTR.insertCell(2);
            newNameTD.innerHTML = "节点值：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(3);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_value' value=''  name='value'   />";

            var newNameTD = newTR.insertCell(4);
            newNameTD.innerHTML = "节点dbname：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(5);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name' value=''  name='dbname'   />";

            var newNameTD = newTR.insertCell(6);
            newNameTD.innerHTML = "节点描述：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(7);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input class='td_name' value=''  name='text'   />";

            var newNameTD = newTR.insertCell(8);
            newNameTD.innerHTML = "启用：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(9);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input type='checkbox' value=''  name='enable'   />";

            var newNameTD = newTR.insertCell(10);
            newNameTD.innerHTML = "公开：";
            newNameTD.className = "td_text_align";
            var newNameTD = newTR.insertCell(11);
            newNameTD.className = "td_input_align";
            newNameTD.innerHTML = "<input  type='checkbox' value=''  name='canget'   />";
            var newNameTD = newTR.insertCell(12);
            newNameTD.className = "td_text_align";
            newNameTD.innerHTML = '<input type="button"  value="删除" onclick="delRow(this)" />';
        }

        //删除一行
        function delRow(obj) {
            //if (!confirm("确定要删除改行数据吗？")) {
                //return;
            //}
            var tr = obj.parentNode.parentNode;
            var tbody = tr.parentNode;
            tbody.removeChild(tr);
            //document.getElementById("tab1").removeros
        }
    </script>

</head>
<body onload="load()">

    <div style="font-size: 30px; font-weight: 700; text-align: center; height: 64px;line-height:64px;">
        数据库中间件配置文件管理
    </div>

    <div style="margin-left:20px;margin-right:20px;">
        <ul id="tab">
            <li class="current">ComSource.xml</li>
            <li>OraDataSource.xml</li>
            <li>SqlDataSource.xml</li>
             <li>MySqlDataSource.xml</li>
        </ul>
        <div id="content">
            <ul style="display: block;">
                <div>
                    <input type="button" style="width: 80px;" value="添加" onclick="addRow(1)" />
                </div>

                <div style="margin-top:5px;">
                    <table id="tab1" style="border: 1px solid; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
                    
                    </table>
                    <input type="button" onclick="saveData(1)" style="width: 80px;margin-top:5px;" value="保存" />

                    
                </div>
            </ul>


            <ul>
                <div >
                    <input type="button" style="width: 80px;" value="添加" onclick="addRow(2)" />
                </div>

                <div style="margin-top:5px;">
                    <table id="tab2" style="border: 1px solid; width: 100%;min-height:20px;height:20px; border-collapse: collapse;" cellspacing="0" cellpadding="0">
                    
                    </table>
                    <input type="button" onclick="saveData(2)" style="width: 80px;margin-top:5px;" value="保存" />
                </div>
            </ul>

            <ul>
               <div >
                    <input type="button" style="width: 80px;" value="添加" onclick="addRow(3)" />
                </div>

                <div style="margin-top:5px;">
                    <table id="tab3" style="border: 1px solid; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
                    
                    </table>
                    <input type="button" onclick="saveData(3)" style="width: 80px;margin-top:5px;" value="保存" />
                </div>
            </ul>

              <ul>
               <div >
                    <input type="button" style="width: 80px;" value="添加" onclick="addRow(4)" />
                </div>

                <div style="margin-top:5px;">
                    <table id="tab4" style="border: 1px solid; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
                    
                    </table>
                    <input type="button" onclick="saveData(4)" style="width: 80px;margin-top:5px;" value="保存" />
                </div>
            </ul>
        </div>

        <div style="margin-top:50px;margin-left:0px;margin-right:0px;">
            粘贴板：
            <textarea  id="txtarea" style="width:100%;height:200px;"  >
                </textarea>
        </div>
    </div>




</body>
</html>
