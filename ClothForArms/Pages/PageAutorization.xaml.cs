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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LopuhV2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAutorization.xaml
    /// </summary>
    public partial class PageAutorization : Page
    {
        public PageAutorization()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            User user = DB.db.User.Where(u => u.Login == tbLogin.Text).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == tbPass.Text)
                {
                    switch (user.RoleID)
                    {
                        case 1:
                            Manager.mainFrame.Navigate(new PageProducts(1));
                            break;
                        case 2:
                            Manager.mainFrame.Navigate(new PageProducts(2));
                            break;
                        case 3:
                            Manager.mainFrame.Navigate(new PageProducts(3));
                            break;
                    }
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageProducts(0));
        }
    }
}
