using System;
using System.Linq;
using Gadget.Data.Entity;

namespace Gadget.Service
{
    using Gadget.Data;
    using Gadget.IService;
    using Gadget.IService.Models;
    using Gadget.Lib.Exception;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AuthorService : IAuthorService
    {
        private readonly GadgetDbContext _dbContext;

        public AuthorService(GadgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAuthor(AuthorModel author)
        {
            if (author == null)
            {
                return await Task.FromException<bool>(new ObjectIsNullException("author is null"));
            }

            if (string.IsNullOrEmpty(author.Email))
            {
                return await Task.FromException<bool>(new ObjectIsNullException("author's email is null"));
            }

            if (await _dbContext.Authors.AnyAsync(a => a.Email.Equals(author.Email.ToUpper())))
            {
                return await Task.FromException<bool>(new ObjectIsNullException("author's email existed"));
            }

            var authorEntity = new Author()
            {
                AvatarUrl = author.AvatarUrl,
                Email = author.Email
            };

            if (author.AuthorName.IndexOf(' ') > 0)
            {
                var names = author.AuthorName.Split(' ');
                authorEntity.FirstName = names[0];
                authorEntity.LastName = string.Join(' ', names.Take(1));
            }
            else
            {
                authorEntity.FirstName = author.AuthorName;
            }

            await _dbContext.Authors.AddAsync(authorEntity);

            return true;
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
