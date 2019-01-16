using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace visitSkive
{

    public class AttractionsList
    {
        public List<Attraction> Attractions;

        public void InsertAttractionsFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {
                cmd.Parameters.Clear();
                if (Attractions[i] != null)
                {
                    int attractionId = Attractions[i].Id;
                    DateTime created = Attractions[i].Created;
                    string createdBy = Attractions[i].CreatedBy;
                    DateTime modified = Attractions[i].Modified;
                    string modifiedBy = Attractions[i].ModifiedBy;
                    DateTime serialized = Attractions[i].Serialized;
                    bool online = Attractions[i].Online;
                    string language = Attractions[i].Language;
                    string name = Attractions[i].Name;
                    string canonicalUrl = Attractions[i].CanonicalUrl;
                    int ownerId = Attractions[i].Owner.Id;
                    int categoryId = Attractions[i].Category.Id;
                    int mainCategoryId = Attractions[i].MainCategory.Id;

                    AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
                    AddParam(cmd, created, "Created", SqlDbType.DateTime);
                    AddParam(cmd, createdBy, "CreatedBy", SqlDbType.NVarChar);
                    AddParam(cmd, modified, "Modified", SqlDbType.DateTime);
                    AddParam(cmd, modifiedBy, "ModifiedBy", SqlDbType.NVarChar);
                    AddParam(cmd, serialized, "Serialized", SqlDbType.DateTime);
                    AddParam(cmd, online, "Online", SqlDbType.Bit);
                    AddParam(cmd, language, "Language", SqlDbType.NVarChar);
                    AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                    AddParam(cmd, canonicalUrl, "CanonicalUrl", SqlDbType.NVarChar);
                    AddParam(cmd, ownerId, "OwnerId", SqlDbType.Int);
                    AddParam(cmd, categoryId, "CategoryId", SqlDbType.Int);
                    AddParam(cmd, mainCategoryId, "MainCategoryId", SqlDbType.Int);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Attractions (attractionId, Created, CreatedBy, Modified, ModifiedBy, Serialized, Online, Language, Name, CanonicalUrl, OwnerId, CategoryId, MainCategoryId ) values" +
                        " (@attractionId, @Created, @CreatedBy, @Modified, @ModifiedBy, @Serialized, @Online, @Language,@Name, @CanonicalUrl, @OwnerId, @CategoryId, @MainCategoryId)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }

        public void InsertContactInformationFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {
                cmd.Parameters.Clear();
                if (Attractions[i].ContactInformation != null)
                {
                    int attractionId = Attractions[i].Id;
                    string phone = Attractions[i].ContactInformation.Phone;
                    string mobile = Attractions[i].ContactInformation.Mobile;
                    string fax = Attractions[i].ContactInformation.Fax;
                    string email = Attractions[i].ContactInformation.Email;
                    AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
                    AddParam(cmd, phone, "Phone", SqlDbType.NVarChar);
                    AddParam(cmd, mobile, "Mobile", SqlDbType.NVarChar);
                    AddParam(cmd, fax, "Fax", SqlDbType.NVarChar);
                    AddParam(cmd, email, "Email", SqlDbType.NVarChar);
                    string linkUrl;
                    if (Attractions[i].ContactInformation.Link != null)
                    {
                        linkUrl = Attractions[i].ContactInformation.Link.Url;
                    }
                    else
                    {
                        linkUrl = null;
                    }

                    AddParam(cmd, linkUrl, "LinkUrl", SqlDbType.NVarChar);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into ContactInformation (AttractionId, Phone, Mobile, Fax, Email, LinkUrl) values (@AttractionId, @Phone,@Mobile, @Fax, @Email, @LinkUrl)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }

        public void InsertCategory()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)
            {
                cmd.Parameters.Clear();
                String name = Attractions[i].Category.Name;
                int categoryId = Attractions[i].Category.Id;
                AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, categoryId, "CategoryId", SqlDbType.Int);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Category (CategoryId, Name) values (@CategoryId, @Name)";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message); 
                }

            }
            con.Close();
        }

        public void InsertMainCategoryFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)
            {
                cmd.Parameters.Clear();
                String name = Attractions[i].MainCategory.Name;
                int mainCategoryId = Attractions[i].MainCategory.Id;
                AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, mainCategoryId, "MainCategoryId", SqlDbType.Int);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "if not exists (select * from MainCategory where MainCategoryId = @MainCategoryId and Name =@Name) insert into MainCategory (MainCategoryId, Name) values (@MainCategoryId, @Name)";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
            con.Close();
        }


        public void InsertOwnerFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {
                cmd.Parameters.Clear();
                string name = Attractions[i].Owner.Name;
                int ownerId = Attractions[i].Owner.Id;
                string address = Attractions[i].Owner.Address;
                string email = Attractions[i].Owner.Email;
                AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, ownerId, "OwnerId", SqlDbType.Int);
                AddParam(cmd, address, "Address", SqlDbType.NVarChar);
                AddParam(cmd, email, "Email", SqlDbType.NVarChar);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Owner (OwnerId, Name, Address, Email) values (@OwnerId, @Name,@Address, @Email)";
                try
                {
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Record added Successfully!");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            con.Close();
        }

        public void InsertDescriptionFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {

                for (int j = 0; j < Attractions[i].Descriptions.Count(); j++)
                {

                    cmd.Parameters.Clear();
                    int attractionId = Attractions[i].Id;
                    int descTypeId = Attractions[i].Descriptions[j].DescriptionTypeID;
                    int descNumber = j;
                    string descType = Attractions[i].Descriptions[j].DescriptionType;
                    string text = Attractions[i].Descriptions[j].Text;
                    string html = Attractions[i].Descriptions[j].Html;

                    AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
                    AddParam(cmd, descTypeId, "DescTypeId", SqlDbType.Int);
                    AddParam(cmd, descNumber, "DescNumber", SqlDbType.Int);
                    AddParam(cmd, descType, "DescType", SqlDbType.NVarChar);
                    AddParam(cmd, text, "Text", SqlDbType.Text);
                    AddParam(cmd, html, "Html", SqlDbType.NVarChar);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Descriptions (attractionId, descTypeId, descNumber, descType, text, html) values (@attractionId, @DescTypeId, @descNumber, @DescType, @Text, @Html)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }

        // Insert Link

        public void InsertLinkFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {
                cmd.Parameters.Clear();
                if (Attractions[i].ContactInformation != null)
                {
                    if (Attractions[i].ContactInformation.Link != null)
                    {
                        int attractionId = Attractions[i].Id;
                        string name = Attractions[i].ContactInformation.Link.Name;
                        string url = Attractions[i].ContactInformation.Link.Url;

                        AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
                        AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                        AddParam(cmd, url, "Url", SqlDbType.NVarChar);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Link (attractionId, Name, Url) values (@attractionId, @Name, @Url)";
                        try
                        {
                            cmd.ExecuteNonQuery();
                            //MessageBox.Show("Record added Successfully!");
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            con.Close();
        }

        //public void InsertContactInformationFromFile()
        //{
        //    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
        //                        + "Integrated Security=true;");
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    con.Open();
        //    for (int i = 0; i < Attractions.Count(); i++)

        //    {
        //        cmd.Parameters.Clear();
        //        if (Attractions[i].ContactInformation != null)
        //        {
        //            int attractionId = Attractions[i].Id;
        //            string phone = Attractions[i].ContactInformation.Phone;
        //            string mobile = Attractions[i].ContactInformation.Mobile;
        //            string fax = Attractions[i].ContactInformation.Fax;
        //            string email = Attractions[i].ContactInformation.Email;                    
        //            AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
        //            AddParam(cmd, phone, "Phone", SqlDbType.NVarChar);
        //            AddParam(cmd, mobile, "Mobile", SqlDbType.NVarChar);
        //            AddParam(cmd, fax, "Fax", SqlDbType.NVarChar);
        //            AddParam(cmd, email, "Email", SqlDbType.NVarChar);
        //            string linkUrl;
        //            if (Attractions[i].ContactInformation.Link != null)
        //            {
        //                 linkUrl = Attractions[i].ContactInformation.Link.Url;
        //            }
        //            else
        //            {
        //                linkUrl = null;
        //            }

        //            AddParam(cmd, linkUrl, "LinkUrl", SqlDbType.NVarChar);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "insert into ContactInformation (AttractionId, Phone, Mobile, Fax, Email, LinkUrl) values (@AttractionId, @Phone,@Mobile, @Fax, @Email, @LinkUrl)";
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //                //MessageBox.Show("Record added Successfully!");
        //            }
        //            catch (Exception ex)
        //            {
        //               // MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //    con.Close();
        //}

        public void InsertAddressFromFile()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            for (int i = 0; i < Attractions.Count(); i++)

            {
                cmd.Parameters.Clear();
                if (Attractions[i].Address != null)
                {
                    int attractionId = Attractions[i].Id;
                    string addressLine1 = Attractions[i].Address.AddressLine1;
                    string addressLine2 = Attractions[i].Address.AddressLine2;
                    int postalCode = Attractions[i].Address.PostalCode;
                    string city = Attractions[i].Address.City;
                    string municipality = Attractions[i].Address.Municipality.Name;
                    string region = Attractions[i].Address.Region;
                    float geoCordinateLat;
                    float geoCordinateLong;

                    if (Attractions[i].Address.GeoCoordinate != null)
                    {
                        geoCordinateLat = Attractions[i].Address.GeoCoordinate.Latitude;
                        geoCordinateLong = Attractions[i].Address.GeoCoordinate.Longitude;
                    }
                    else
                    {
                        geoCordinateLat = 0;
                        geoCordinateLong = 0;
                    }
                    AddParam(cmd, attractionId, "AttractionId", SqlDbType.Int);
                    AddParam(cmd, addressLine1, "AddressLine1", SqlDbType.NVarChar);
                    AddParam(cmd, addressLine2, "AddressLine2", SqlDbType.NVarChar);
                    AddParam(cmd, postalCode, "PostalCode", SqlDbType.Int);
                    AddParam(cmd, city, "City", SqlDbType.NVarChar);
                    AddParam(cmd, municipality, "Municipality", SqlDbType.NVarChar);
                    AddParam(cmd, region, "Region", SqlDbType.NVarChar);
                    AddParam(cmd, geoCordinateLat, "GeoCordinateLat", SqlDbType.Float);
                    AddParam(cmd, geoCordinateLong, "GeoCordinateLong", SqlDbType.Float);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Address (AttractionId, AddressLine1, AddressLine2, PostalCode, City, Municipality,Region, GeoCordinateLatitude, GeoCordinateLongitude ) values (@AttractionId, @AddressLine1, @AddressLine2, @PostalCode, @City, @Municipality, @Region, @GeoCordinateLat, @GeoCordinateLong)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }




        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + name;
            if (value != null)
            {
                parameter.Value = value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            parameter.SqlDbType = sqlDbType;
            parameter.Size = 255;
            cmd.Parameters.Add(parameter);
        }
    }


    //public class Rootobject
    //{
    //    public Attraction[] Attraction { get; set; }
    //}

    public class Attraction :INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Serialized { get; set; }
        public bool Online { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string CanonicalUrl { get; set; }
        public Owner Owner { get; set; }
        public Category Category { get; set; }
        public Maincategory MainCategory { get; set; }
        public Address Address { get; set; }
        public Contactinformation ContactInformation { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<FileName> Files { get; set; }
        public List<Socialmedialink> SocialMediaLinks { get; set; }
        public List<Bookinglink> BookingLinks { get; set; }
        public List<Externallink> ExternalLinks { get; set; }
        public List<Metatag> MetaTags { get; set; }
        public List<Relatedproduct> RelatedProducts { get; set; }
        public List<Place> Places { get; set; }
        public List<Mediachannel> MediaChannels { get; set; }
        public List<object> Distances { get; set; }
        public int Priority { get; set; }
        public List<Period> Periods { get; set; }
        public Periodslink PeriodsLink { get; set; }
        public List<Pricegroup> PriceGroups { get; set; }
        public Pricegroupslink PriceGroupsLink { get; set; }
        public List<Route> Routes { get; set; }
        public List<Room> Rooms { get; set; }
        public int? Capacity { get; set; }
        //, DateTime Created, string CreatedBy, DateTime Modified, string ModifiedBy,

        public Attraction()
        {
            Owner = new Owner();
            Category = new Category();
            MainCategory = new Maincategory();
            Address = new Address();
            ContactInformation = new Contactinformation();
            Descriptions = new List<Description>();
        }
        public Attraction(int id, string name)
        {
            Id = id;
            Name = name;
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        

    }

    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Owner()
        {

        }
        //public Owner(int id, string name, string address, string email)
        //{
        //    Id = id;
        //    Name = name;
        //    Address = address;
        //    Email = email;
        //}
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
        }
    }

    public class Maincategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public Municipality Municipality { get; set; }
        public string Region { get; set; }
        public Geocoordinate GeoCoordinate { get; set; }

        public Address() {
            Municipality = new Municipality();
            GeoCoordinate = new Geocoordinate();
        }
    }

    public class Municipality
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Geocoordinate
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class Contactinformation
    {
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Link Link { get; set; }
        public Contactinformation() {
            Link = new Link();
        }
    }

    public class Link
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Periodslink
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Pricegroupslink
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Description
    {
        public int DescriptionTypeID { get; set; }
        public string DescriptionType { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
    }

    public class FileName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Uri { get; set; }
        public string Copyright { get; set; }
        public string Photographer { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
        public string MetaTag { get; set; }
    }

    public class Socialmedialink
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Bookinglink
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Externallink
    {
        public string LinkType { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }

    public class Metatag
    {
        public Metataggroup MetaTagGroup { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }

    public class Metataggroup
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string CanonicalName { get; set; }
        public bool UseInSearch { get; set; }
        public bool ShowOnWeb { get; set; }
    }

    public class Relatedproduct
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
    }

    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Mediachannel
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
    }

    public class Period
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public List<int?> PriceGroupIds { get; set; }
    }

    public class Pricegroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? PriceFrom { get; set; }
        public float? PriceTo { get; set; }
        public bool? Free { get; set; }
        public string PriceLevel { get; set; }
        public int?[] PeriodIds { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Geocoordinate1> GeoCoordinates { get; set; }
    }

    public class Geocoordinate1
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
