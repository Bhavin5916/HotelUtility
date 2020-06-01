using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Demo.Hotel.handlers
{

    class Confighandler
    {

        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adp ;
        SqlDataReader dr ;
        SqlConnection con = connection.Con();
        public int table_No { get; set; }
        public string Adress { get; set; }
        public string Phone_no { get; set; }
        public decimal taxRate { get; set; }
        public decimal Taxadditionthresold { get; set; }
        public decimal billcolumnsize { get; set; }

        public void getconfig()
        {
            dt = new DataTable();
            con.Open();
            string str;
            str = "select * from tbl_configration";
            cmd = new SqlCommand(str, con);
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            con.Close();
            table_No = Convert.ToInt16(dt.Rows[0].ItemArray[1]);//dt.Rows(0).Item(7);
            Adress = dt.Rows[0].ItemArray[1].ToString();
            Phone_no = dt.Rows[0].ItemArray[2].ToString();
            taxRate = Convert.ToDecimal(dt.Rows[0].ItemArray[3].ToString());
            Taxadditionthresold = Convert.ToDecimal(dt.Rows[0].ItemArray[8]);
            billcolumnsize = Convert.ToDecimal(dt.Rows[0].ItemArray[9]);
        }
    }
}
