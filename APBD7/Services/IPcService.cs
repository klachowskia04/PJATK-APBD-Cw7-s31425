using APBD_CW7.DTOs;

namespace APBD_CW7.Services;

public interface IPcService
{
    Task<List<PcDto>> GetAllAsync();

    Task<PcDetailsDto?> GetComponentsAsync(int id);

    Task<PcDto> CreateAsync(PcCreateUpdateDto dto);

    Task<bool> UpdateAsync(int id, PcCreateUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}