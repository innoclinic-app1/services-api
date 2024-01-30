using Domain.Dtos.Categories;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var category = await _service.GetOneAsync(id);

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany(int skip, int take)
    {
        var categories = await _service.GetManyAsync(skip, take);

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOne(CategoryCreateDto dto)
    {
        var category = await _service.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetOne), new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOne(int id, CategoryUpdateDto dto)
    {
        var category = await _service.UpdateAsync(id, dto);
        
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}
