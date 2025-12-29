using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace BAZA_PODATAKA_PROIZVOD
{
    public class BP
    {
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command;

        public BP()
        {
            connection.ConnectionString =
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Proizvod;Integrated Security=True;TrustServerCertificate=True;";
            command = connection.CreateCommand();
        }

        public int GetMaxId()
        {
            try
            {
                connection.Open();
                command.CommandText = "SELECT ISNULL(MAX(ID_Proizvoda), 0) FROM Proizvod";
                int maxId = Convert.ToInt32(command.ExecuteScalar());
                return maxId;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public void SnimanjePodataka(Proizvod proizvod)
        {
            try
            {
                connection.Open();

                // Kolone u bazi: NazivProizvoda, Pakovanje, Sadrzaj (ako su ti tako i dalje)
                command.CommandText =
                    $"INSERT INTO Proizvod VALUES ({proizvod.ID_Proizvoda}, '{proizvod.Naziv}', '{proizvod.JedinicaMere}', '{proizvod.Opis}')";

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show("Doslo je do greske: " + ex);
            }
        }

        public List<Proizvod> CitanjePodataka()
        {
            var proizvodi = new List<Proizvod>();

            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Proizvod";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var proizvod = new Proizvod();

                    proizvod.ID_Proizvoda = Convert.ToInt32(reader["ID_Proizvoda"]);

                    // Mapiranje iz kolona baze u nove property-je:
                    proizvod.Naziv = reader["NazivProizvoda"].ToString();
                    proizvod.JedinicaMere = reader["Pakovanje"].ToString();
                    proizvod.Opis = reader["Sadrzaj"].ToString();

                    proizvodi.Add(proizvod);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show("Doslo je do greske: " + ex);
            }

            return proizvodi;
        }

        public void Brisanje(Proizvod p)
        {
            try
            {
                connection.Open();
                command.Parameters.Clear();

                command.CommandText = $"DELETE FROM Proizvod WHERE ID_Proizvoda = {p.ID_Proizvoda}";
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show("Doslo je do greske: " + ex);
            }
        }

        public void Update(Proizvod p)
        {
            try
            {
                connection.Open();

                // Kolone u bazi i dalje NazivProizvoda, Pakovanje, Sadrzaj
                command.CommandText =
                    $"UPDATE Proizvod " +
                    $"SET NazivProizvoda = '{p.Naziv}', " +
                    $"Pakovanje = '{p.JedinicaMere}', " +
                    $"Sadrzaj = '{p.Opis}' " +
                    $"WHERE ID_Proizvoda = {p.ID_Proizvoda}";

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show("Doslo je do greske: " + ex);
            }
        }
    }
}
