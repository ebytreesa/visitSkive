using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visitSkive
{
    public class AttractionOwner
    {        
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string OwnerName { get; set; } 

        public AttractionOwner(int id, string name, string ownerName)
        {
            Id = id;
            Name = name;
            OwnerName = ownerName;

        }

        public static List<AttractionOwner> getAttractionOwner()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.AttractionId, a.Name, o.Name as OwnerName from Attractions a inner join Owner o on a.OwnerId = o.OwnerId ";
            con.Open();
            reader = cmd.ExecuteReader();
            List<AttractionOwner> attractions = new List<AttractionOwner>();
            while (reader.Read())
            {
                attractions.Add(new AttractionOwner((int)reader[0], reader[1].ToString(), reader[2].ToString()));
            }

            con.Close();
            return attractions;
        }

        public static AttractionOwner getAttractionOwnerSelected(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.AttractionId, a.Name, o.Name as OwnerName from Attractions a inner join Owner o on a.OwnerId = o.OwnerId  where a.AttractionId=@id";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            con.Open();
            reader = cmd.ExecuteReader();
            //List<AttractionOwner> attractions = new List<AttractionOwner>();
            AttractionOwner selectedItem = new AttractionOwner(1, "", "");
            while (reader.Read())
            {
                selectedItem.Id = (int)reader[0];
                selectedItem.Name = reader[1].ToString();
                selectedItem.OwnerName = reader[2].ToString();
                //selectedItem.Add(new AttractionOwner((int)reader[0], reader[1].ToString(), reader[2].ToString()));
            }
                con.Close();
                return selectedItem;

        }


    }
}


