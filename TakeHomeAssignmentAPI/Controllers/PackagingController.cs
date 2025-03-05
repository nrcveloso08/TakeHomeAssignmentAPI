using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TakeHomeAssignmentAPI.Services;
using TakeHomeAssignmentAPI.DTO;
using AutoMapper;
using TakeHomeAssignmentAPI.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/v1/packages")]
public class PackageController : ControllerBase
{
    private readonly IPackagingService _packagingService;
    private readonly IMapper _mapper;

    public PackageController(IPackagingService packagingService, IMapper mapper)
    {
        _packagingService = packagingService;
        _mapper = mapper;
    }

    // GET: api/v1/packages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAllPackages()
    {
        var packages = await _packagingService.GetAllPackagingsAsync();
        return Ok(_mapper.Map<IEnumerable<PackageDTO>>(packages));
    }

    // GET: api/v1/packages/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PackageDTO>> GetPackageById(int id)
    {
        var package = await _packagingService.GetPackagingByIdAsync(id);
        if (package == null)
            return NotFound();

        return Ok(_mapper.Map<PackageDTO>(package));
    }

    // POST: api/v1/packages
    [HttpPost]
    public async Task<ActionResult<PackageDTO>> CreatePackage([FromBody] PackageDTO packageDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var package = _mapper.Map<Packaging>(packageDTO);
        var createdPackage = await _packagingService.CreatePackageAsync(package);

        return CreatedAtAction(nameof(GetPackageById), new { id = createdPackage.Id }, _mapper.Map<PackageDTO>(createdPackage));
    }

    // PUT: api/v1/packages/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageDTO packageDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingPackage = await _packagingService.GetPackagingByIdAsync(id);
        if (existingPackage == null)
            return NotFound();

        _mapper.Map(packageDTO, existingPackage);
        await _packagingService.UpdatePackagingAsync(existingPackage);

        return NoContent();
    }

    // DELETE: api/v1/packages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePackage(int id)
    {
        var existingPackage = await _packagingService.GetPackagingByIdAsync(id);
        if (existingPackage == null)
            return NotFound();

        await _packagingService.DeletePackagingAsync(id);
        return NoContent();
    }
}
