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
                String query = "SELECT COUNT(1) FROM [Owner] WHERE Name=@Name AND OwnerId=@OwnerID";
                SqlCommand sqlCmd = new SqlCommand(query, SqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Name", User.Text);
                sqlCmd.Parameters.AddWithValue("@OwnerID", PassWord.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                if (count == 1)
                {
                    while (reader.Read())
                    {
                        
                       // attractions.Add(new Attraction((int)reader[0], reader[8].ToString()));
                    }
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect");
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
