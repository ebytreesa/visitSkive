using System;
using System.Collections.Generic;
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
    /// Interaction logic for ShowData.xaml
    /// </summary>
    public partial class ShowDataList : Window
    {
        public int userId { get; set; }

        public ShowDataList(int id)
        {
            userId = id;
            InitializeComponent();
            List<AttractionOwner> att = AttractionOwner.getAttractionOwner(userId);
            //DALAttraction.showAttractions();   
            lvAttractions.ItemsSource = att;
        }

        private void lvAttractions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AttractionOwner selected = lvAttractions.SelectedItem as AttractionOwner;      
            AttractionOwner.getAttractionOwnerSelected(selected.Id);
            //Test selected = lvAttractions.SelectedItem as Test;
            //Test.TestSelected(selected.Att.Id);
            ShowSelected selectedItemView = new ShowSelected(selected);
            selectedItemView.Show();
            this.Close();

        }
        


    }
}
