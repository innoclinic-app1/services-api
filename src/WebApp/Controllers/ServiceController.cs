using Domain.Dtos.Services;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _service;
    
    public ServiceController(IServiceService service)
    {
        _service = service;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var service = await _service.GetOneAsync(id);

        return Ok(service);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMany(int skip, int take)
    {
        var services = await _service.GetManyAsync(skip, take);

        return Ok(services);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOne(ServiceCreateDto dto)
    {
        var service = await _service.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetOne), new { id = service.Id }, service);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOne(int id, ServiceUpdateDto dto)
    {
        var service = await _service.UpdateAsync(id, dto);
        
        return Ok(service);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}
