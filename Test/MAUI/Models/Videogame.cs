using System.ComponentModel.DataAnnotations;

namespace MAUI.Models;

public class Videogame
{
    [Key]
    public int IdVideoGame{ get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public string Company { get; set; }
    public string Gender { get; set; }
}
