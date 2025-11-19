using System.ComponentModel.DataAnnotations;

namespace Groeiproject.UI.Web.Models;

public class NewBakeryViewModel
{
    [Key]
    public int Id { get; set; }
    [Required][StringLength(50, MinimumLength = 4)]
    public string Adress { get; set; }
    [Required]
    public string Name { get; set; }
}