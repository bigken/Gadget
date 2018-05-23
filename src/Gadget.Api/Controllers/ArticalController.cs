namespace Gadget.Api.Controllers
{
    using Gadget.Api.Models;
    using Gadget.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/v1/articals")]
    public class ArticalController : Controller
    {
        private readonly GadgetDbContext _dbContext;

        public ArticalController(GadgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ArticalModel[]), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var result = _dbContext.Articles.Select(x => new ArticalModel()
            {
                ArticalId = x.Id,
                AuthorId = x.Author.Id,
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                AuthorAvatar = x.Author.AvatarUrl,
                Title = x.Title,
                PublishedDateTime = x.PublishedDateTime
            }).ToArray();

            return Ok(result);
        }
    }
}
