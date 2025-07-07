using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(
        IGenericRepository<T> repo,
        ISpecification<T> spec,
        int pageIndex,
        int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec); // Get the total results.
        var count = await repo.CountAsync(spec); // Get the total results count.
        
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items); // Paginate the results.
        return Ok(pagination);
    }
}