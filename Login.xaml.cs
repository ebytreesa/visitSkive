using System;
using System.Collections.Generic;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(@"Data Source =.; Initial Catalog = visitSkive; Integrated Security=true;");
            try
            {
                if (SqlCon.State == System.Data.ConnectionState.Closed)
                    SqlCon.Open();
                String query = "SELECT * FROM [Owner] WHERE OwnerId=@OwnerID AND Name=@Name ";
                SqlCommand sqlCmd = new SqlCommand(query, SqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Name", User.Text);
                sqlCmd.Parameters.AddWithValue("@OwnerID", PassWord.Text);
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();
                //Owner owner = new Owner(1, "", "","");
                Owner owner = new Owner();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        owner.Id = (int)reader[0];
                    }

                    MainWindow dashboard = new MainWindow(owner.Id);
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    // MessageBox.Show("Username or password is incorrect");
                    //MessageBox.Show("Username not logged in. Showing data for id 482");
                    owner.Id = 482;
                    MainWindow dashboard = new MainWindow(owner.Id);
                    dashboard.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SqlCon.Close();
              
            }


        }
    }
    }
