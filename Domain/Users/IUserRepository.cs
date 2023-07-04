using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id, CancellationToken cancellationToken = default);

        Task<User> GetUserByUuid(string uuid, CancellationToken cancellationToken = default);

        Task<User> CreateUser(User user, CancellationToken cancellationToken = default);

        Task<User> UpdateUser(User user, CancellationToken cancellationToken = default);

        Task<User> DeleteUser(int id, CancellationToken cancellationToken = default);
    }
}
