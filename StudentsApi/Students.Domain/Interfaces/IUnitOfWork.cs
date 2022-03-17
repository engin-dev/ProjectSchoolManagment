using System.Threading.Tasks;

namespace Students.Domain.Interfaces;

public interface IUnitOfWork{

    Task<int> CommitAsync();
}