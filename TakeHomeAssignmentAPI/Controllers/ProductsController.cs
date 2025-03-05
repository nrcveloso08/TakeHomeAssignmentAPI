using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TakeHomeAssignmentAPI.Data;
using TakeHomeAssignmentAPI.DTO;
using TakeHomeAssignmentAPI.Models;

[Authorize]
[ApiController]
[Route("api/v1/products")]
[ApiVersion("1.0")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    
    public ProductController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ GET All Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
    }


    // ✅ GET Product by ID (Including Packaging)
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Packaging)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(_mapper.Map<ProductDTO>(product));
    }

    // ✅ CREATE New Product
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] ProductDTO DTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var product = _mapper.Map<Product>(DTO);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _mapper.Map<ProductDTO>(product));
    }

    // ✅ UPDATE Product
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO DTO)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        _mapper.Map(DTO, product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ✅ DELETE Product
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
