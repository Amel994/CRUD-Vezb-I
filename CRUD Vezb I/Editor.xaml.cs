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

namespace CRUD_Vezb_I
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            DataContext = new Artikal();
            BindingGroup = new BindingGroup();
            InitializeComponent();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            if(BindingGroup.CommitEdit())
            {
                DialogResult = true;
                this.Close();
            }
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
