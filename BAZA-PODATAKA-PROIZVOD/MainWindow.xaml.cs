using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BAZA_PODATAKA_PROIZVOD
{
    public partial class MainWindow : Window
    {
        private Proizvod proizvod;
        private BP bp;

        public ObservableCollection<Proizvod> Proizvodi { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Proizvod Proizvod
        {
            get { return proizvod; }
            set
            {
                proizvod = value;
                OnPropertyChanged();
            }
        }

        public BP BP
        {
            get { return bp; }
            set { bp = value; }
        }

        public MainWindow()
        {
            BP = new BP();
            Proizvodi = new ObservableCollection<Proizvod>();

            Proizvod.brojac = BP.GetMaxId();
            Proizvod = new Proizvod();

            InitializeComponent();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ADD
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BP == null) { MessageBox.Show("BP je null"); return; }
            if (Proizvodi == null) { MessageBox.Show("Proizvodi je null"); return; }
            if (Proizvod == null) { MessageBox.Show("Proizvod je null"); return; }

            Proizvod p = new Proizvod
            {
                Naziv = Proizvod.Naziv,
                JedinicaMere = Proizvod.JedinicaMere,
                Opis = Proizvod.Opis
            };

            BP.SnimanjePodataka(p);
            Proizvodi.Add(p);
        }

        // LOAD
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Proizvodi.Clear();
            var citanjeProizvoda = BP.CitanjePodataka();

            foreach (Proizvod p in citanjeProizvoda)
                Proizvodi.Add(p);
        }

        // DELETE
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selected = dgProizvodi.SelectedItem as Proizvod;
            if (selected == null)
            {
                MessageBox.Show("Moraš selektovati red u tabeli da bi brisao.");
                return;
            }

            BP.Brisanje(selected);
            Proizvodi.Remove(selected);
        }

        // UPDATE
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selected = dgProizvodi.SelectedItem as Proizvod;
            if (selected == null)
            {
                MessageBox.Show("Moraš selektovati red u tabeli da bi update izvršio.");
                return;
            }

            BP.Update(selected);
            dgProizvodi.SelectedItem = null;
        }
    }
}
