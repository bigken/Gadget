using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gadget.Data;
using Microsoft.AspNetCore.Mvc;

namespace Gadget.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly GadgetDbContext _dbContext;

        public AuthorController(GadgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _dbContext.Authors.Select(x => x.Name);
        }
    }
}
