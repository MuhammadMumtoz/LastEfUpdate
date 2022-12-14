namespace Domain.Entities;
using Domain.Entities;
public class Region
{
    public int RegionId { get; set; }
    public string RegionName { get; set; }

    public virtual List<Country> Countries { get; set; }
}