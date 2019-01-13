using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visitSkive
{
    static class DALCategory
    {
        public static List<Category> showCategory()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Category";
            con.Open();
            reader = cmd.ExecuteReader();
            List<Category> categories = new List<Category>();

            while (reader.Read())
            {
                categories.Add(new Category( (int)reader[0], reader[1].ToString()));
            }
            con.Close();
            return categories;
        }

    }
}
