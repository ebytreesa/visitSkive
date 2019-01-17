using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace visitSkive
{
    /// <summary>
    /// Interaction logic for viewAttraction.xaml
    /// </summary>
    public partial class viewAttraction : Window
    {
        public Attraction Selected { get; set; }
        public int userId { get; set; }


        public viewAttraction(Attraction selected, int id)
        {
            InitializeComponent();
            userId = id;
            Selected = selected;


            List<Category> catList = DALCategory.getCategoryList();
            
            List<string> names = new List<string>();
            
                foreach (Category cat in catList)
                {
                    string name = cat.Name;
                    names.Add(name);
                }
                category.ItemsSource = names;
            if (name != null)
            {

                category.SelectedItem = Selected.Category.Name.ToString();
            }

            name.Text = Selected.Name.ToString();
            language.Text = Selected.Language.ToString();
            canoniacalUrl.Text = Selected.CanonicalUrl.ToString();
            if (Selected.Online == true)
            {
                online.SelectedIndex = 1;
            }
            else
            {
                online.SelectedIndex = 0;
            }
            //category.Text = Selected.Category.Name.ToString();
            //mainCategory.Text = Selected.MainCategory.Name.ToString();
            addressline1.Text = Selected.Address.AddressLine1.ToString();
            addressline2.Text = Selected.Address.AddressLine2.ToString();
            postalCode.Text = Selected.Address.PostalCode.ToString();
            city.Text = Selected.Address.City.ToString();
            municippality.Text = Selected.Address.Municipality.Name.ToString();
            region.Text = Selected.Address.Region.ToString();
            geoLat.Text = Selected.Address.GeoCoordinate.Latitude.ToString();
            geoLong.Text = Selected.Address.GeoCoordinate.Longitude.ToString();
            phone.Text = Selected.ContactInformation.Phone.ToString();
            mobile.Text = Selected.ContactInformation.Mobile.ToString();
            fax.Text = Selected.ContactInformation.Fax.ToString();
            email.Text = Selected.ContactInformation.Email.ToString();
            linkurl.Text = Selected.ContactInformation.Link.Url.ToString();           

        }
        
       

        private void UpdateDataButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                    + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            
            cmd.Parameters.Add("@AttractionId", SqlDbType.Int).Value = Selected.Id;

            cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Online", SqlDbType.Bit).Value = online.SelectedIndex;
            cmd.Parameters.Add("@OwnerId", SqlDbType.NVarChar).Value = userId;


            AddParam(cmd, name.Text, "Name", SqlDbType.NVarChar);
            AddParam(cmd, language.Text, "Language", SqlDbType.NVarChar);
            AddParam(cmd, canoniacalUrl.Text, "CanonicalUrl", SqlDbType.NVarChar);

            //cmd.Parameters.Add("@CatName", SqlDbType.NVarChar).Value = category.SelectedItem;

            int catId = DALCategory.GetCatId(category.SelectedItem.ToString());
            cmd.Parameters.Add("@CatId", SqlDbType.Int).Value = catId;


            AddParam(cmd, addressline1.Text, "Addressline1", SqlDbType.NVarChar);
            AddParam(cmd, addressline2.Text, "Addressline2", SqlDbType.NVarChar);
            AddParam(cmd, municippality.Text, "Municippality", SqlDbType.NVarChar);
            AddParam(cmd, city.Text, "City", SqlDbType.NVarChar);
            AddParam(cmd, region.Text, "Region", SqlDbType.NVarChar);
            //AddParam(cmd, postalCode.Text, "postalCode", SqlDbType.Int);
            int postalint = 0;
            try
            {
                 postalint = int.Parse(postalCode.Text);
                
            }
            catch (Exception ex)
            {
                //postalint = 0;
            }
            AddParam(cmd, postalint, "postalCode", SqlDbType.Int);
            AddParam(cmd, geoLat.Text, "GeoLat", SqlDbType.Float);
            AddParam(cmd, geoLong.Text, "GeoLong", SqlDbType.Float);

            AddParam(cmd, phone.Text, "Phone", SqlDbType.NVarChar);
            AddParam(cmd, mobile.Text, "Mobile", SqlDbType.NVarChar);
            AddParam(cmd, fax.Text, "Fax", SqlDbType.NVarChar);
            AddParam(cmd, email.Text, "Email", SqlDbType.NVarChar);
            AddParam(cmd, linkurl.Text, "Linkurl", SqlDbType.NVarChar);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Attractions set Modified = @Modified,Online = @Online, OwnerId =@OwnerId, CategoryId=@CatId, Name = @Name," +
                                 " Language =@Language,CanonicalUrl =@CanonicalUrl  where AttractionId = @AttractionId ;" +

                              "update  Address set Addressline1 = @Addressline1,Addressline2 =@Addressline2, " +
                                "Municipality =@Municippality, City=@City, postalCode =@postalCode," +
                                "Region=@Region, GeoCordinateLongitude=@GeoLong, GeoCordinateLatitude = @GeoLat where AttractionId = @AttractionId ;" +

                              "update ContactInformation set Phone=@Phone ,Mobile =@Mobile," +
                               "Fax =@Fax, Email= @Email, Linkurl =@Linkurl where AttractionId = @AttractionId ";
                            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            // Open attraction list view
            ShowDataList listView = new ShowDataList(userId);
            listView.Show();
            MessageBox.Show("Data updated");
            this.Close();
        }

        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            parameter.SqlDbType = sqlDbType;
            parameter.Size = 255;
            cmd.Parameters.Add(parameter);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ShowDataList listView = new ShowDataList(userId);
            listView.Show();
            this.Close();
        }


        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sure?");

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=visitSkive;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.Parameters.Add("@AttractionId", SqlDbType.Int).Value = Selected.Id;

            //AddParam(cmd, name, "Name", SqlDbType.NVarChar);
            //AddParam(cmd, age, "Age", SqlDbType.Int);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Attractions  where AttractionId = @AttractionId;" +
                              " delete from Address  where AttractionId = @AttractionId;" +
                              "delete from ContactInformation  where AttractionId = @AttractionId";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            // Open attraction list view
            ShowDataList listView = new ShowDataList(userId);
            listView.Show();
            MessageBox.Show("Data deleted");
            this.Close();
        }

    }
}
