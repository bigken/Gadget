namespace Gadget.Service
{
    using System.Threading.Tasks;
    using Gadget.Data;
    using Gadget.IService;
    using Gadget.IService.Models;
    using Microsoft.EntityFrameworkCore;

    public class AuthorService : IAuthorService
    {
        private readonly GadgetDbContext _dbContext;

        public AuthorService(GadgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthorModel> GetAuthor(long authorId)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            if (author == null)
            {
                return null;
            }

            return new AuthorModel()
            {
                AuthorId = author.Id,
                AuthorName = $"{author.FirstName} {author.LastName}",
                AvatarUrl = author.AvatarUrl,
                Email = author.Email
            };
        }
    }
}
