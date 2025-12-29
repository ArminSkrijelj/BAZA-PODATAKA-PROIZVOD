using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BAZA_PODATAKA_PROIZVOD
{
    public class Proizvod : INotifyPropertyChanged
    {
        private int id_Proizvoda;
        private string naziv;
        private string jedinicaMere;
        private string opis;

        public static int brojac = 0;

        public int ID_Proizvoda
        {
            get { return id_Proizvoda; }
            set
            {
                id_Proizvoda = value;
                OnPropertyChanged();
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged();
            }
        }

        public string JedinicaMere
        {
            get { return jedinicaMere; }
            set
            {
                jedinicaMere = value;
                OnPropertyChanged();
            }
        }

        public string Opis
        {
            get { return opis; }
            set
            {
                opis = value;
                OnPropertyChanged();
            }
        }

        public Proizvod()
        {
            ID_Proizvoda = ++brojac;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
