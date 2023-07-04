using Commons.UnitOfWork;
using Domain.Users;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistent.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork uow;

        public UserRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<User> CreateUser(User user, CancellationToken cancellationToken = default)
        {
            var sql = "INSERT INTO users(user_name, password) VALUES(@user_name, @password)";
            MySqlCommand? command = this.uow.CreateCommand("") as MySqlCommand;
            command!.Parameters.AddWithValue("@user_name", user.UserName);
            command!.Parameters.AddWithValue("@password", user.Password);

            await command.ExecuteNonQueryAsync(cancellationToken);
            int lastInsertedId = (int)command.LastInsertedId;

            sql = "SELECT * FROM users WHERE id = @id";
            command = this.uow.CreateCommand(sql) as MySqlCommand;

            command!.Parameters.AddWithValue("@id", lastInsertedId);
            await using (var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    var insertedUser = new User
                    {
                        Id = await reader.GetFieldValueAsync<int>("id", cancellationToken),
                        Uuid = Guid.Parse(await reader.GetFieldValueAsync<string>("uuid", cancellationToken)),
                        UserName = await reader.GetFieldValueAsync<string>("user_name", cancellationToken),
                        Password = await reader.GetFieldValueAsync<string>("password", cancellationToken),
                        Email = await reader.IsDBNullAsync("email") ? null : await reader.GetFieldValueAsync<string>("email", cancellationToken),
                        StaffId = await reader.IsDBNullAsync("staff_id") ? null : await reader.GetFieldValueAsync<int>("staff_id", cancellationToken),
                        Enabled = await reader.GetFieldValueAsync<bool>("enabled", cancellationToken)
                    };

                    return insertedUser;
                }
            }

            throw new InvalidOperationException("Can retrieval inserted row.");
        }

        public Task<User> DeleteUser(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUuid(string uuid, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
