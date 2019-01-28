using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Models
{
public class Film
{
    public short FilmID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }
}
}
