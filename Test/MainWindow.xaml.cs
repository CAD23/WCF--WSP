using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test.ServiceReference3;

//using Test.ServiceReference3;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ServiceReference1.DbServiceClient clien = new ServiceReference1.DbServiceClient();
            //clien.Test();
            try
            {
              ServiceReference3.DbServiceClient client = new ServiceReference3.DbServiceClient();
               // ServiceReference2.OraDbServiceClient client = new ServiceReference2.OraDbServiceClient();
                //int Pindex, string Psql, int Psize, out int Pcount, out int Prowcount
                //CmdParameter[] paras = new CmdParameter[] { 
                //    dbc.CmdParameter("@Pindex",0),
                //    dbc.CmdParameter("@Psql",menu.LABEL_TYPE_ID),
                //    dbc.CmdParameter("@Psize",menu.LABEL_NAME),  
                //    dbc.CmdParameter("@Pcount",menu.LABEL_CODE),
                // dbc.CmdParameter("@Prowcount",menu.LABEL_CODE)
                //}
                //DataTable dt = client.getDataBySql(" select * from CL_WLQD ");


                        //OracleParameterCollection pars = new OracleParameterCollection();
                        //pars.Add(new OracleParameter("PSQL", OracleType.VarChar, 500));
                        //pars["PSQL"].Value = " 1=1 ";
                        //pars["PSQL"].Direction = ParameterDirection.Input;

                        //pars.Add(new OracleParameter("v_cur", OracleType.Cursor));

                        //pars["v_cur"].Direction = ParameterDirection.Output;

                        //OracleParameter[] paras = new OracleParameter[]{
                        //    new OracleParameter("PSQL",OracleType.VarChar,500),
                        //    new OracleParameter("v_cur", OracleType.Cursor)
                        //};
                        //paras[0].Value = " 1=1 ";
                        //paras[0].Direction = ParameterDirection.Input;

                        //paras[1].Direction = ParameterDirection.Output;// System.Data.OracleClient.OracleType.VarChar;
                       
                //       };
                //      ();
                //paras.["PSQL"].Value = " 1=1 ";
                //        da.SelectCommand.Parameters["PSQL"].Direction = ParameterDirection.Input;

                //        da.SelectCommand.Parameters.Add(new OracleParameter("v_cur", OracleType.Cursor));

                //        da.SelectCommand.Parameters["v_cur"].Direction = ParameterDirection.Output;


                        //DataTable dt2 = client.GetTableByProc(" BIM.CL_WLQD_GET", paras);

                //新增数据
//                        OracleParameter[] paras = new OracleParameter[]{
//                            new OracleParameter("WL_GUID",OracleType.VarChar,50),
//                            new OracleParameter("CLFL_GUID",OracleType.VarChar,50),
//                            new OracleParameter("ZLX",OracleType.VarChar,300),
//                            new OracleParameter("GG",OracleType.VarChar,300),
//                            new OracleParameter("CLXH",OracleType.VarChar,300),
//                            new OracleParameter("CD",OracleType.VarChar,300),
//                            new OracleParameter("KD",OracleType.VarChar,300),
//                            new OracleParameter("GD",OracleType.VarChar,300),
//                            new OracleParameter("GCL",OracleType.Number),
//                            new OracleParameter("DW",OracleType.VarChar,300),
//                            new OracleParameter("BZ",OracleType.VarChar,800),
//                            new OracleParameter("RESULTVALUE", OracleType.Int32)
//                        };
//                        paras[0].Value = " ASDAS2233";
//                       // paras[0].Direction = ParameterDirection.Input;

//                        paras[1].Value = "";
////paras[1].Direction = ParameterDirection.Input;

//                        paras[2].Value = " ASDAS";
//                        paras[3].Value = " ASDAS";
//                        paras[4].Value = " ASDAS";
//                        paras[5].Value = " ASDAS";
//                        paras[6].Value = " ASDAS";
//                        paras[7].Value = " ASDAS";
//                        paras[8].Value = 33;
//                        paras[9].Value = " ASDAS";
//                        paras[10].Value = " ASDAS";
//                        paras[11].Direction = ParameterDirection.Output;// System.Data.OracleClient.OracleType.VarChar;

//                        int vv = client.ExcuteProc("BIM.CL_WLQD_ADD", paras);


                client.init("PlatFormDB", "ORACLE");
                if (1 == 2) { 
                
                    }
                CmdParameter[] paras = new CmdParameter[] { 
                   client.CmdParameter("PSQL","  1=1 ","Varchar","ORACLE",500,ParameterDirection.Input),
                   client.CmdParameter("v_cur","","Cursor","ORACLE",500,ParameterDirection.Output)
                     
                };
                DataTable dt2 = client.GetTableByProc("PlatForm_Auth_User.SP_SYS_USER_GET", paras);
                int i = 0;



                //dbc.init(Utility.Funtcions.getConfigValue("PlatFormDB"));
               // CmdParameter[] paras = new CmdParameter[] { dbc.CmdParameter("@VALUE",uWhere)
                  //  };
                //CmdParameter[] paras2 = new CmdParameter[] { new CmdParameter(){ParameterName= "@VALUE",Value =uWhere}
                //    };
                //DataTable dt = dbc.GetTableByProc("SP_SYS_USER_GET", paras);
                //ServiceReference4.SqlDbServiceClient client = new ServiceReference4.SqlDbServiceClient();
              //  client.init("PlatFormDB");
               // CmdParameter[] paras = new CmdParameter[] { client.CmdParameter("@VALUE"," and  1=1 ")
                 //   };
              //  DataTable dt = client.GetTableByProc("SP_SYS_USER_GET", paras);

               
                //client = new ServiceReference3.DbServiceClient();
                //client.init("PlatFormDB", "SQLSERVER");
                //CmdParameter[] pars = new CmdParameter[1] ;



                //CmdParameter cm = client.CmdParameter("@VALUE", " AND 1=1 ", "Varchar", "SQLSERVER", 500, ParameterDirection.Input);

                //pars[0] = cm;
                //DataTable dt = client.GetTableByProc("SP_SYS_USER_GET", pars);

                int k = 0;
            }
            catch (Exception ex)  { 
            
            }
        }
    }
}
