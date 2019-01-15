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
            //List<DALAttraction> att = DALAttraction.getAttractionsList(userId);
            List<Attraction> att = DALAttraction.getAttractionsList(userId);
            //DALAttraction.showAttractions();   
            lvAttractions.ItemsSource = att;
            

        }

        private void lvAttractions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //DALAttraction selected = lvAttractions.SelectedItem as DALAttraction;      
            Attraction selected = lvAttractions.SelectedItem as Attraction;      
            DALAttraction.GetSelected(selected.Id);
            //ShowSelected selectedItemView = new ShowSelected(selected);
            //selectedItemView.Show();
            viewAttraction selectedItemView = new viewAttraction(selected);
            selectedItemView.Show();
            this.Close();
             

        }

    

}
}
