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

namespace ClothForArms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        public PageMaterials()
        {
            InitializeComponent();
            var currentMaterials = DB.db.Material.ToList();
            var types = DB.db.MaterialType.ToList();
            types.Insert(0, new MaterialType { Title = "Все типы" });
            List<string> sort = new List<string>();

            sort.Add("Сортировка");
            sort.Add("По наименованию");
            sort.Add("По остатку на складе");
            sort.Add("По стоимости");

            cbTypes.ItemsSource = types;
            cbTypes.DisplayMemberPath = "Title";
            cbTypes.SelectedValuePath = "Id";

            cbSort.ItemsSource = sort;

            cbSort.SelectedIndex = 0;
            cbTypes.SelectedIndex = 0;

            rbAsc.IsEnabled = false;
            rbDesc.IsEnabled = false;

            lvMaterials.ItemsSource = currentMaterials;

        }

        private void UpdateMaterials()
        {
            var currentMaterials = DB.db.Material.ToList();

            if (cbTypes.SelectedIndex > 0)
            {
                currentMaterials = currentMaterials.Where(m => m.MaterialTypeId == int.Parse(cbTypes.SelectedValue.ToString())).ToList();
            }

            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        rbAsc.IsEnabled = true;
                        rbDesc.IsEnabled = true;
                        if (rbAsc.IsChecked == true)
                            currentMaterials = currentMaterials.OrderBy(m => m.Title).ToList();
                        else
                            currentMaterials = currentMaterials.OrderByDescending(m => m.Title).ToList();
                        break;
                    case 2:
                        rbAsc.IsEnabled = true;
                        rbDesc.IsEnabled = true;
                        if (rbAsc.IsChecked == true)
                            currentMaterials = currentMaterials.OrderBy(m => m.CountInStock).ToList();
                        else
                            currentMaterials = currentMaterials.OrderByDescending(m => m.CountInStock).ToList();
                        break;
                    case 3:
                        rbAsc.IsEnabled = true;
                        rbDesc.IsEnabled = true;
                        if (rbAsc.IsChecked == true)
                            currentMaterials = currentMaterials.OrderBy(m => m.Cost).ToList();
                        else
                            currentMaterials = currentMaterials.OrderByDescending(m => m.Cost).ToList();
                        break;
                }
            }
            else
            {
                rbAsc.IsEnabled = false;
                rbDesc.IsEnabled = false;
            }

            if (tbFinder.Text != null)
            {
                currentMaterials = currentMaterials.Where(m => m.Title.Contains(tbFinder.Text)).ToList();
            }

            lvMaterials.ItemsSource = currentMaterials;

            tbRecordsCount.Text = lvMaterials.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Material.Count().ToString();

            
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterials();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterials();
        }

        private void rbAsc_Click(object sender, RoutedEventArgs e)
        {
            UpdateMaterials();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEdit(new Material()));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEdit((sender as Button).DataContext as Material));
        }

        private void lvMaterials_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DB.db.ChangeTracker.Entries().ToList().ForEach(m => m.Reload());
            lvMaterials.ItemsSource = DB.db.Material.ToList();

            tbRecordsCount.Text = lvMaterials.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Material.Count().ToString();
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMaterials();
        }
    }
}
