using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BadWebServer.Models;

namespace BadWebServer.Controllers
{
    public class SakilaController : Controller
    {
[HttpGet]
public IActionResult Index()
{

    //Unfortunately, no MySQL client for .NET in the Core Framework, so download one via NuGet: MySql.Data
    string connectionString = @"server=codingtempletest.crzzw12s7omo.us-east-1.rds.amazonaws.com;uid=root;pwd=password;database=sakila";
    //ADO.Net 
    MySqlConnection sqlConnection = new MySqlConnection(connectionString);
    sqlConnection.Open();
    MySqlCommand selectCommand = sqlConnection.CreateCommand();
    selectCommand.CommandText = "SELECT *  FROM sakila.film;";
    MySqlDataReader reader = selectCommand.ExecuteReader();
    List<Film> films = new List<Film>();

    int idColumn = reader.GetOrdinal("film_id");
    int titleColumn = reader.GetOrdinal("title");
    int releaseYearColumn = reader.GetOrdinal("release_year");
    int descriptionColumn = reader.GetOrdinal("description");

    while (reader.Read())
    {
        films.Add(new Film
        {
            FilmID = reader.GetInt16(idColumn),
            Title = reader.GetString(titleColumn),
            Description = reader.GetString(descriptionColumn),
            ReleaseYear = reader.GetInt32(releaseYearColumn)
        });
    }
    sqlConnection.Close();
    return Json(films);
}
    }
}
