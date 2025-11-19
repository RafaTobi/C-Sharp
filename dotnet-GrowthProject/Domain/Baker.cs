using System.ComponentModel.DataAnnotations;

namespace Groeiproject.BL.Domain;

public class Baker
{
    [Key]
    public int Id { get; set; }
    [Required][StringLength(10, MinimumLength = 2)]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Required]
    public List<Contract> Contracts = new();
}