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
    /// Interaction logic for ShowSelected.xaml
    /// </summary>
    public partial class ShowSelected : Window
    {
        public AttractionOwner Selected { get; set; }

        //public Test  Selected{get; set;}

        //public ShowSelected(Test selected)
        //{
        //    InitializeComponent();
        //    Selected = selected;
        //    Id.Text = Selected.Att.Id.ToString();
        //    Name.Text = Selected.Att.Name;
        //    CreatedBy.Text = selected.Ow.Name;

        //}


        public ShowSelected(AttractionOwner selected)
        {
            InitializeComponent();
            Selected = selected;
            Id.Text = Selected.Id.ToString();
            Name.Text = Selected.Name;
            CreatedBy.Text = selected.OwnerName;
        }
    }
}
