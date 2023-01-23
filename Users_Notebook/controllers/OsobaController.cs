using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Users_Notebook.Models;

namespace Users_Notebook.Controllers
{
    public class OsobaController : Controller
    {
        private readonly IConfiguration _configuration;
        public OsobaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult AddOsoba()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOsoba(User osoba, UserAttribute osobaAtrybut)
        {
            if (!ModelState.IsValid)
            {
                return View(osoba);
            }
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DateTime date = DateTime.Parse(osoba.Data_urodzenia);

                string query = "INSERT INTO Osoby (Imie, Nazwisko, Data_urodzenia, Plec) VALUES (@Imie, @Nazwisko, @Data_urodzenia, @Plec); SELECT CAST(SCOPE_IDENTITY() as int)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Imie", osoba.Imie);
                command.Parameters.AddWithValue("@Nazwisko", osoba.Nazwisko);
                command.Parameters.AddWithValue("@Data_urodzenia", osoba.Data_urodzenia);
                command.Parameters.AddWithValue("@Plec", osoba.Plec);
                int newOsobaId = (int)command.ExecuteScalar();

                query = "INSERT INTO Atrybuty_osób (Osoba_id, Atrybut, Wartosc) VALUES (@Osoba_id, @Atrybut, @Wartosc)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Osoba_id", newOsobaId);
                command.Parameters.AddWithValue("@Atrybut", osobaAtrybut.Atrybut);
                command.Parameters.AddWithValue("@Wartosc", osobaAtrybut.Wartosc);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditOsoba(int id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            User osoba = new User();
            UserAttribute osobaAtrybut = new UserAttribute();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Osoby WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    osoba.Id = id;
                    osoba.Imie = reader["Imie"].ToString();
                    osoba.Nazwisko = reader["Nazwisko"].ToString();
                    osoba.Data_urodzenia = reader["Data_urodzenia"].ToString();
                    osoba.Plec = reader["Plec"].ToString();
                }
                reader.Close();


                query = "SELECT * FROM Atrybuty_osób WHERE Osoba_id = @id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    osobaAtrybut.Atrybut = reader["Atrybut"].ToString();
                    osobaAtrybut.Wartosc = reader["Wartosc"].ToString();
                }
                reader.Close();

            }
            return View(Tuple.Create(osoba, osobaAtrybut));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]

        public ActionResult EditOsoba(User osoba, UserAttribute osobaAtrybut)
        {
            if (!ModelState.IsValid)
            {
                return View(osoba);
            }
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Osoby SET Imie = @Imie, Nazwisko = @Nazwisko, Data_urodzenia = @Data_urodzenia,Plec = @Plec WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Imie", osoba.Imie);
                command.Parameters.AddWithValue("@Nazwisko", osoba.Nazwisko);
                command.Parameters.AddWithValue("@Data_urodzenia", osoba.Data_urodzenia);
                command.Parameters.AddWithValue("@Plec", osoba.Plec);
                command.Parameters.AddWithValue("@Id", osoba.Id);
                command.ExecuteNonQuery();

                query = "UPDATE Atrybuty_osób SET Atrybut = @Atrybut, Wartosc = @Wartosc WHERE Osoba_id = @Osoba_id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Atrybut", osobaAtrybut.Atrybut);
                command.Parameters.AddWithValue("@Wartosc", osobaAtrybut.Wartosc);
                command.Parameters.AddWithValue("@Osoba_id", osoba.Id);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}



 