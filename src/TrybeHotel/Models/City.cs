namespace TrybeHotel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    // 1. Implemente as models da aplicação
    public class City {
    [Key]
    public int CityId { get; set; }

    [Required]
    public string? Name  { get; set; }
    
    [Required]
    public String? State { get; set; }

    public ICollection<Hotel>? Hotels { get; set; }

    }
}