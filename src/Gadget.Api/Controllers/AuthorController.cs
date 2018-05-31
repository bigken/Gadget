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
        public async Task<AuthorModel> Get(long id)
        {
            return await _authorService.GetAuthor(id);
        }
    }
}