using APP.Users.Features.Users;
using APP.USERS.Features.Users;
using CORE.APP.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.BLOGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //get users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //IQueryable<UserQueryResponse> query = await _mediator.Send(new UserQueryRequest());
            //List<UserQueryResponse> list = await query.ToListAsync();
            //if (list.Count > 0) 
            //    return Ok(list);
            //return NoContent();
            try
            {
                var response = await _mediator.Send(new UserQueryRequest());
                var list = await response.ToListAsync();
                if (list.Any())
                    return Ok(list);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("UsersGet Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during UsersGet."));
            }
        }
        //get blog 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _mediator.Send(new UserQueryRequest());
                var item = await response.SingleOrDefaultAsync(r => r.Id == id);
                if (item is not null)
                    return Ok(item);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("UsersGetById Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during UsersGetById."));
            }
        }

        // POST: Blogs


        // token
        [HttpPost("[action]")] //api users token
        [AllowAnonymous]
        public async Task<IActionResult> Token(TokenRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.IsSuccessful)
                        return Ok(response);
                    ModelState.AddModelError("UsersToken", response.Message);
                }
                return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
            }
            catch (Exception exception)
            {
                _logger.LogError("UsersToken Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during UsersToken."));
            }
        }
    }

}
