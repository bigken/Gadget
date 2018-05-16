using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gadget.Data;
using Microsoft.AspNetCore.Mvc;

namespace Gadget.Api.Controllers
{
    [Route("api/v1/authors")]
    public class AuthorController : Controller
    {
        private readonly GadgetDbContext _dbContext;

        public AuthorController(GadgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), 200)]
        [ProducesResponseType(404)]
        public IEnumerable<string> Get()
        {
            return _dbContext.Authors.Select(x => x.Name);
        }
    }
}
