using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.BL.Domain;

public class Bakery
{
    [Key]
    public int Id { get; set; }
    [MinLength(4)]
    public string Adres { get; set; }
    [Required]
    public string Name { get; set; }

    public List<Bread> Breads = new();
    [Required]
    public List<Contract> Contracts = new();
    
    public IdentityUser Maintainer { get; set; }
}