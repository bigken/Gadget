namespace Gadget.Api.Controllers
{
    using System.Threading.Tasks;
    using Gadget.IService;
    using Gadget.IService.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthorModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _authorService.GetAuthor(id));
        }

        public async Task<IActionResult> Post([FromBody] AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorService.AddAuthor(author);

            return Ok();
        }
    }
}