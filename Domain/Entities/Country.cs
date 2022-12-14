namespace Domain.Entities;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Country{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(2)]
    [Required]
    public string CountryId { get; set; }
    public string CountryName { get; set; }
    public int RegionId { get; set; }
    public virtual Region Region { get; set; }
    public virtual List<Location> Locations { get; set; }
}