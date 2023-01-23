using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Users_Notebook.Models;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Users_Notebook.controllers
{
    public class DataController : Controller
    {
        private readonly IConfiguration _configuration;
        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public ActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Osoby.Imie, Osoby.Nazwisko, Osoby.Data_urodzenia, Osoby.Plec, Atrybuty_osób.Atrybut, Atrybuty_osób.Wartosc FROM Osoby JOIN Atrybuty_osób ON Osoby.Id = Atrybuty_osób.Osoba_id";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<User> dataList = new List<User>();
                while (reader.Read())
                {
                    User data = new User();
                    data.Imie = reader["Imie"].ToString();
                    data.Nazwisko = reader["Nazwisko"].ToString();
                    data.Data_urodzenia = reader["Data_urodzenia"].ToString();
                    data.Plec = reader["Plec"].ToString();
                    data.Atrybut = reader["Atrybut"].ToString();
                    data.Wartosc = reader["Wartosc"].ToString();
                    dataList.Add(data);
                }
                ViewBag.data = dataList;
            }
            return View();
        }
        public ActionResult ExportUsersReport()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Imie, Nazwisko, Data_urodzenia, Plec FROM Osoby";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<UserReport> reportList = new List<UserReport>();
                while (reader.Read())
                {
                    UserReport report = new UserReport();
                    report.Imie = reader["Imie"].ToString();
                    report.Nazwisko = reader["Nazwisko"].ToString();
                    report.Data_urodzenia = (DateTime)reader["Data_urodzenia"];
                    report.Plec = reader["Plec"].ToString();
                    report.Tytul = report.Plec == "M" ? "Pan" : "Pani";
                    report.Wiek = (int)((DateTime.Now - report.Data_urodzenia).TotalDays / 365.25);
                    reportList.Add(report);
                }
                using(var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                    csv.WriteRecords(reportList);
                    streamWriter.Flush();
                    return File(memoryStream.ToArray(), "text/csv", "DataGenerowaniaRaportuZDokładnościąDoSekund.csv");
                }
            }
        }
    }

}

