using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace visitSkive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int userId { get; set; }

        public MainWindow(int id)
        {
             userId = id;           
            InitializeComponent();
            text.Text = userId.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // read file into a string and deserialize JSON to a typeS

            AttractionsList skive = JsonConvert.DeserializeObject<AttractionsList>(File.ReadAllText(@"C:\Users\eby\Documents\dania\visitSkive\skive_json.json"),
           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            try
            {
                //skive.InsertOwnerFromFile();
                //skive.InsertLinkFromFile();
                //skive.InsertCategory();
                //skive.InsertContactInformationFromFile();
                //skive.InsertDescriptionFromFile();
                //skive.InsertMainCategoryFromFile();
                //skive.InsertAddressFromFile();
                //skive.InsertAttractionsFromFile();
               // List<Category> cat = DALCategory.showCategory();
                //MessageBox.Show("Data er hentet");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            int id = userId;
            ShowDataList listView = new ShowDataList(userId);
            listView.Show();
            this.Close();

        }
    }
}
