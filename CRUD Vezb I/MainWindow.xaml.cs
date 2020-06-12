using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CRUD_Vezb_I
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Artikal> listaArt = new ObservableCollection<Artikal>();

        public MainWindow()
        {
            InitializeComponent();
            dg.ItemsSource = listaArt;

            listaArt.Add(new Artikal("Plazma", 50, 30));
            listaArt.Add(new Artikal("Smoki", 20, 20));
            listaArt.Add(new Artikal("Cips", 60, 40));
            listaArt.Add(new Artikal("Bonzita", 30, 20));

        }

        private void Dodavanje(object sender, RoutedEventArgs e)
        {
            Editor Edit = new Editor();
            Edit.Owner = this;

            if (Edit.ShowDialog() == true)
            {
                listaArt.Add(Edit.DataContext as Artikal);
            }
             
             
        }

        private void Izmena(object sender, RoutedEventArgs e)
        {
            if(dg.SelectedItem != null)
            {
                Editor Edit = new Editor();
                Edit.Owner = this;
                Edit.DataContext = dg.SelectedItem;
                Edit.ShowDialog();
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            if(dg.SelectedItem != null)
            {
                listaArt.Remove(dg.SelectedItem as Artikal);
            }
        }
    }
}
