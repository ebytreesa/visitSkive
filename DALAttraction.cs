using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visitSkive
{
    public static class DALAttraction
    {
        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
        public static Double SafeGetDouble(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return (float)reader.GetSqlDouble(colIndex);
            return 0;
        }

        // get selected attraction from attractions list
        public static Attraction GetSelected(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            string var = "select a.AttractionId, a.Created, a.CreatedBy, a.Modified, a.ModifiedBy," +
                              "a.Serialized, a.Online, a.Language, a.Name, a.CanonicalUrl," +
                              " o.Name as OwnerName, cat.Name as CatName, m.Name as MainCatName," +
                              " ad.*, ci.*" +
                              " from Attractions a inner join owner o on a.OwnerId = o.OwnerId " +
                              " left join Category cat on a.CategoryId = cat.CategoryId" +
                              " left join MainCategory m on a.MainCategoryId = m.mainCategoryId" +
                              " left join Address ad on a.AttractionId = ad.AttractionId" +
                              " left  join ContactInformation ci on a.AttractionId = ci.AttractionId" +
                              " where a.AttractionId = @id";
            cmd.CommandText = var;

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            con.Open();
            reader = cmd.ExecuteReader();
            Attraction selectedItem = new Attraction();
            while (reader.Read())
            {
                selectedItem.Id = (int)reader[0];
                selectedItem.Created =(DateTime) reader[1];
                selectedItem.CreatedBy = reader[2].ToString();
                selectedItem.Modified = (DateTime)reader[3];
                selectedItem.ModifiedBy = reader[4].ToString();
                selectedItem.Serialized = (DateTime)reader[5];
                selectedItem.Online = (bool)reader[6];
                selectedItem.Language = reader[7].ToString();
                selectedItem.Name = reader[8].ToString();
                selectedItem.CanonicalUrl = reader[9].ToString();
                selectedItem.Owner.Name = reader[10].ToString();
                selectedItem.Category.Name = reader[11].ToString();
                selectedItem.MainCategory.Name = reader[12].ToString();
                selectedItem.Address.AddressLine1 = reader[14].ToString();
                selectedItem.Address.AddressLine2 = reader[15].ToString();
                selectedItem.Address.PostalCode =(int) reader[16];
                selectedItem.Address.City = reader[17].ToString();
                selectedItem.Address.Municipality.Name= reader[18].ToString();
                selectedItem.Address.Region = reader[19].ToString();
                selectedItem.Address.GeoCoordinate.Latitude = (float) SafeGetDouble(reader,20);
                selectedItem.Address.GeoCoordinate.Longitude =(float) SafeGetDouble(reader, 21);
                selectedItem.ContactInformation.Phone = reader[23].ToString();
                selectedItem.ContactInformation.Mobile = reader[24].ToString();
                selectedItem.ContactInformation.Fax = reader[25].ToString();
                selectedItem.ContactInformation.Email = reader[26].ToString();
                selectedItem.ContactInformation.Link.Url = reader[27].ToString();

            }
            con.Close();
            return selectedItem;

        }

        public static List<Attraction> getAttractionsList(int id)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=VisitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select a.AttractionId, a.Name, o.Name as OwnerName, c.Name  as CatName from Attractions a" +
                " inner join Owner o on a.OwnerId = o.OwnerId  "+
                "left join Category c on a.CategoryId = c.CategoryId where o.OwnerId=@id  ";

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            con.Open();
            reader = cmd.ExecuteReader();
            List<Attraction> attractions = new List<Attraction>();
            while (reader.Read())
            {
                Attraction Att = new Attraction();
                Att.Id = (int)reader[0]; ;
                Att.Name = reader[1].ToString();
                //Owner Ow = new Owner();
                Att.Owner.Name = reader[2].ToString();
                Att.Category.Name = reader[3].ToString();
                attractions.Add(Att); 

                //DALAttraction AO = new DALAttraction();
                //AO.Att.Id = (int)reader[0]; ;
                //AO.Att.Name = reader[1].ToString();
                //AO.Ow.Name = reader[2].ToString();
                //attractions.Add(AO);
            }

            con.Close();
            return attractions;
        }
       
    }
    
}



