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
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        Material currentMaterial = new Material();

        public PageAddEdit(Material selectedMaterial)
        {
            InitializeComponent();

            if (selectedMaterial != null)
            {
                currentMaterial = selectedMaterial;
            }

            cbTypes.ItemsSource = DB.db.MaterialType.ToList();

            DataContext = currentMaterial;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(tbTitle.Text))
                errors.AppendLine("Укажите наименование");
            if (cbTypes.SelectedItem == null)
                errors.AppendLine("Укажите тип");
            if (string.IsNullOrEmpty(tbCountInStock.Text))
                errors.AppendLine("Укажите кол-во на складе");
            if (string.IsNullOrEmpty(tbCost.Text))
                errors.AppendLine("Укажите стоимость");
            if (string.IsNullOrEmpty(tbCountInPack.Text))
                errors.AppendLine("Укажите кол-во в упаковке");
            if (string.IsNullOrEmpty(tbMinCount.Text))
                errors.AppendLine("Укажите мин. кол-во");
            if (string.IsNullOrEmpty(tbUnit.Text))
                errors.AppendLine("Укажите единицу измерения");
            if (int.Parse(tbMinCount.Text) < 0)
                errors.AppendLine("Укажите корректные данные мин. кол-ва");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 

            if (currentMaterial.Id == 0)
            {
                DB.db.Material.Add(currentMaterial);
            }

            try
            {
                DB.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.db.Material.Remove(currentMaterial);
                    DB.db.SaveChanges();
                    MessageBox.Show("Данные удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
