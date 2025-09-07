using Microsoft.AspNetCore.Mvc;
using TodoListApp.Application.DTOs;
using TodoListApp.Application.Interfaces;

namespace TodoListApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemsController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemServices.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _itemServices.GetItemByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
        {
            var item = await _itemServices.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateItemDto dto)
        {
            var success = await _itemServices.UpdateItemAsync(id, dto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _itemServices.DeleteItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
