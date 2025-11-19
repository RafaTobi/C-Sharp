namespace Groeiproject.UI.Web.Models.Dto;

public class ContractDTO
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Price { get; set; }
    public int BakeryId { get; set; }
    public int BakerId { get; set; }
}