using Domain.Dtos.Specializations;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("specializations")]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _service;
    
    public SpecializationController(ISpecializationService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var specialization = await _service.GetOneAsync(id);

        return Ok(specialization);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMany(int skip, int take)
    {
        var specializations = await _service.GetManyAsync(skip, take);

        return Ok(specializations);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOne(SpecializationCreateDto dto)
    {
        var specialization = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetOne), new { id = specialization.Id }, specialization);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOne(int id, SpecializationUpdateDto dto)
    {
        var specialization = await _service.UpdateAsync(id, dto);

        return Ok(specialization);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
