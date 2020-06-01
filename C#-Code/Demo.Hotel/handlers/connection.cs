using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Demo.Hotel
{
    class connection
    {
        public static SqlConnection Con()
        {
            SqlConnection conection=new SqlConnection(@"Data Source=..\SQLEXPRESS;AttachDbFilename=|DataDirectory|\data\db_restorent.mdf;Integrated Security=True;User Instance=True");

            return conection; 
        }
    }
}
