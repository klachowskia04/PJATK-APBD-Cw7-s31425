using APBD_CW7.DTOs;
using APBD_CW7.Services;
using Microsoft.AspNetCore.Mvc;
namespace APBD_CW7.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PcDto>>> GetAll()
    {
        var pcs = await _pcService.GetAllAsync();

        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<ActionResult<PcDetailsDto>> GetComponents(int id)
    {
        var pc = await _pcService.GetComponentsAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PcDto>> Create(PcCreateUpdateDto dto)
    {
        var createdPc = await _pcService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetComponents),
            new { id = createdPc.Id },
            createdPc
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PcCreateUpdateDto dto)
    {
        var updated = await _pcService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}