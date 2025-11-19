using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Groeiproject.BL.Domain;

public class Bread
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Price { get; set; }
    [Range(50, 1000)] //gewicht in gram
    public int Weight { get; set; }
    public DateTime DateOfProduction { get; set; }
    public Size Size;
    [NotMapped]
    public Bakery Bakery;
}