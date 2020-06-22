using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //public ObservableCollection<Artikal> listaArt = new ObservableCollection<Artikal>();
        EF baza = new EF();

        private int _trenutnaSifra;
        public int TrenutnaSifra
        {
            get => _trenutnaSifra;
            set
            {
                _trenutnaSifra = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrenutnaSifra"));
            }
        }

        private int _trenutnaKolicina;
        public int TrenutnaKolicina
        {
            get => _trenutnaKolicina;
            set
            {
                _trenutnaKolicina = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrenutnaKolicina"));
            }
        }

        public Racun TrenutniRacun = new Racun();

        

        public event PropertyChangedEventHandler PropertyChanged;

        private string _pretraga;
        public string Pretraga
        {
            get => _pretraga;
            set
            {
                _pretraga = value;
                if (string.IsNullOrWhiteSpace(_pretraga))

                    dg.ItemsSource = baza.Artikals.ToList(); 
                else

                    dg.ItemsSource = baza.Artikals.Where(a => a.Naziv.Contains(_pretraga.Trim())).ToList();
            }
        }
           

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            dg.ItemsSource = baza.Artikals.ToList();
            StackUnos.BindingGroup = new BindingGroup();
            StackIzdavanje.DataContext = TrenutniRacun;
            baza.Racuns.ToList();
            dgRacuni.ItemsSource = baza.Racuns.Local;
            

        }

        private void Dodavanje(object sender, RoutedEventArgs e)
        {
            
            Editor Edit = new Editor();
            Edit.Owner = this;

            if (Edit.ShowDialog() == true)
            {
                baza.Artikals.Add(Edit.DataContext as Artikal);
                baza.SaveChanges();
                dg.ItemsSource = baza.Artikals.ToList();
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
                baza.SaveChanges();
                
                dg.ItemsSource = baza.Artikals.ToList();
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            if(dg.SelectedItem != null)
            {

                baza.Artikals.Remove(dg.SelectedItem as Artikal);
                baza.SaveChanges();
                dg.ItemsSource = baza.Artikals.ToList();
            }
        }

        private void UnosArtKol(object sender, RoutedEventArgs e)
        {
            if (StackUnos.BindingGroup.CommitEdit())
            {
                var art = baza.Artikals.Find(TrenutnaSifra);

                if(art != null)
                {
                    var ak = new ArtKol(art, TrenutnaKolicina);
                    TrenutniRacun.ArtikliKolicina.Add(ak);
                    dgStavke.ItemsSource = null;
                    dgStavke.ItemsSource = TrenutniRacun.ArtikliKolicina;
                    TrenutnaSifra = 0;
                    TrenutnaKolicina = 0;
                }else
                {
                    MessageBox.Show("Ne postoji artikal sa tom sifrom!");
                }

            }else
            {
                MessageBox.Show("Proverite unesene podatke!");
            }
            
        }

        private void IzdavanjeRacuna(object sender, RoutedEventArgs e)
        {
            TrenutniRacun.Datum = DateTime.Now;
            TrenutniRacun.ArtikliKolicina.ForEach(ak => baza.ArtKols.Add(ak));
            baza.Racuns.Add(TrenutniRacun);
            baza.SaveChanges();
            TrenutniRacun = new Racun();
            dgStavke.ItemsSource = null;

        }
    }
}
