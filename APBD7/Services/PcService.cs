using APBD_CW7.Data;
using APBD_CW7.DTOs;
using APBD_CW7.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW7.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcDto>> GetAllAsync()
    {
        return await _context.Pcs
            .Select(pc => new PcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PcDetailsDto?> GetComponentsAsync(int id)
    {
        return await _context.Pcs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcDetailsDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PcComponents.Select(pcComponent => new PcComponentDto
                {
                    Amount = pcComponent.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcComponent.Component.Code,
                        Name = pcComponent.Component.Name,
                        Description = pcComponent.Component.Description,
                        Manufacturer = new ManufacturerDto
                        {
                            Id = pcComponent.Component.ComponentManufacturer.Id,
                            Abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcComponent.Component.ComponentManufacturer.FullName,
                            FoundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pcComponent.Component.ComponentType.Id,
                            Abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                            Name = pcComponent.Component.ComponentType.Name
                        }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PcDto> CreateAsync(PcCreateUpdateDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(int id, PcCreateUpdateDto dto)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}