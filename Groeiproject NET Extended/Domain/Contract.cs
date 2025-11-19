using System.ComponentModel.DataAnnotations;

namespace Groeiproject.BL.Domain;

public class Contract
{
    [Key]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Price { get; set; }
    
    [Required]
    public Bakery Bakery { get; set; }
    [Required]
    public Baker Baker { get; set; }
}
