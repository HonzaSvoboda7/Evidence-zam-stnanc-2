using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpravaZamestnancu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Zamestnanec : INotifyPropertyChanged
    {
        private string _jmeno;
        private string _prijmeni;
        private int _id;
        private DateTime _datumNarozeni;

        public string Jmeno
        {
            get { return _jmeno; }
            set
            {
                _jmeno = value;
                OnPropertyChanged("Jmeno");
            }
        }

        public string Prijmeni
        {
            get { return _prijmeni; }
            set
            {
                _prijmeni = value;
                OnPropertyChanged("Prijmeni");
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public DateTime DatumNarozeni
        {
            get { return _datumNarozeni; }
            set
            {
                _datumNarozeni = value;
                OnPropertyChanged("DatumNarozeni");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class MainWindow : Window
    {
        private ObservableCollection<Zamestnanec> _zamestnanci = new ObservableCollection<Zamestnanec>();

        public MainWindow()
        {
            InitializeComponent();
            SeznamZamestnancu.ItemsSource = _zamestnanci;
        }

        private void PridatZamestnance_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addWindow = new AddEmployeeWindow();
            if (addWindow.ShowDialog() == true)
            {
                Zamestnanec zamestnanec = new Zamestnanec();
                zamestnanec.Jmeno = addWindow.JmenoTextBox.Text;
                zamestnanec.Prijmeni = addWindow.PrijmeniTextBox.Text;
                zamestnanec.ID = int.Parse(addWindow.IDTextBox.Text);
               

                _zamestnanci.Add(zamestnanec);
            }
        }

        private void UpravitZamestnance_Click(object sender, RoutedEventArgs e)
        {
            if (SeznamZamestnancu.SelectedItem != null)
            {
                Zamestnanec selectedEmployee = (Zamestnanec)SeznamZamestnancu.SelectedItem;
                EditEmployeeWindow editWindow = new EditEmployeeWindow();
                editWindow.JmenoTextBox.Text = selectedEmployee.Jmeno;
                editWindow.PrijmeniTextBox.Text = selectedEmployee.Prijmeni;
                editWindow.IDTextBox.Text = selectedEmployee.ID.ToString();

                if (editWindow.ShowDialog() == true)
                {
                    selectedEmployee.Jmeno = editWindow.JmenoTextBox.Text;
                    selectedEmployee.Prijmeni = editWindow.PrijmeniTextBox.Text;
                    selectedEmployee.ID = int.Parse(editWindow.IDTextBox.Text);
                }
            }
        }
        private void OdebratZamestnance_Click(object sender, RoutedEventArgs e)
        {
            if (SeznamZamestnancu.SelectedItem != null)
            {
                _zamestnanci.Remove((Zamestnanec)SeznamZamestnancu.SelectedItem);
            }
        }
        private void ZobrazitDetaily_Click(object sender, RoutedEventArgs e)
        {
            if (SeznamZamestnancu.SelectedItem != null)
            {
                Zamestnanec zamestnanec = (Zamestnanec)SeznamZamestnancu.SelectedItem;
                MessageBox.Show($"ID: {zamestnanec.ID}\nJméno: {zamestnanec.Jmeno} {zamestnanec.Prijmeni}\nDatum narození: {zamestnanec.DatumNarozeni.ToShortDateString()}", "Detaily zaměstnance");
            }
        }
    }
}
