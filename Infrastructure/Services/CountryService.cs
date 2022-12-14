using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
public class CountryService
{
    private readonly DataContext _context;
    public CountryService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<GetCountryDto>>> GetCountries()
    {
        var list = await _context.Countries.Select(s => new GetCountryDto()
        {
            CountryId = s.CountryId,
            CountryName = s.CountryName,
            RegionName = s.Region.RegionName
        }).ToListAsync();
        return new Response<List<GetCountryDto>>(list.ToList());
    }
   
    public async Task<Response<AddCountryDto>> InsertCountry(AddCountryDto country)
    {
        var newCountry = new Country()
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName,
            RegionId = country.RegionId
        };
        _context.Countries.Add(newCountry);
        await _context.SaveChangesAsync();
        country.CountryId = newCountry.CountryId;
        return new Response<AddCountryDto>(country);
    }

    public async Task<Response<AddCountryDto>> UpdateCountry(AddCountryDto country)
    {
        var find = await _context.Countries.FindAsync(country.CountryId);
        // find.CountryId = country.CountryId;
        find.CountryName = country.CountryName;
        find.RegionId = country.RegionId;
        var updated = await _context.SaveChangesAsync();
        return new Response<AddCountryDto>(country);
    }
    public async Task<Response<string>> DeleteCountry(string id)
    {
        var find = await _context.Countries.FindAsync(id);
        _context.Remove(find);
        var response = await _context.SaveChangesAsync();
        if(response>0)
                return new Response<string>("Object deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Object not found");
    }
    
}