using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visitSkive
{
    class DALAttraction
    {

        public static List<Attraction> getAttractions()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *  from Attractions ";
            con.Open();
            reader = cmd.ExecuteReader();
            List<Attraction> attractions = new List<Attraction>();
            while (reader.Read())
            {                
                attractions.Add(new Attraction((int)reader[0], reader[8].ToString()));
            }
           
            con.Close();
            return attractions;
        }
    }
}


