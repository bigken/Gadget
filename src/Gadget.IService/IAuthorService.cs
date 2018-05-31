using System.Threading.Tasks;
using Gadget.IService.Models;

namespace Gadget.IService
{
    public interface IAuthorService
    {
        Task<AuthorModel> GetAuthor(long authorId);
    }
}
