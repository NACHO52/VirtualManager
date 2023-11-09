using Dapper;
using System.Data;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public class SystemUserDAO : ISystemUserDAO
    {
        private readonly IDbConnection _dbConnection;
        public SystemUserDAO(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IList<SystemUser>> GetAll()
        {
            string sql = @"SELECT Id, Username, [Password], Email, [Status] FROM SystemUser";

            return (IList<SystemUser>)await _dbConnection.QueryAsync<SystemUser>(sql, new { });
        }
        public async Task<SystemUser> Get(int id)
        {
            string sql = "SELECT Id, Username, [Password], Email, [Status] FROM SystemUser WHERE Id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<SystemUser>(sql, new { id });
        }
        public async Task Save(SystemUser obj)
        {
            string sql = "INSERT INTO SystemUser (Username, [Password], Email, [Status]) VALUES (@username, @password, @email, @status)";
            var result = await _dbConnection.ExecuteAsync(sql, new { username = obj.Username, password = obj.Password, email = obj.Email, status = obj.Status});
        }
        public async Task Delete(int id)
        {
            string sql = "UPDATE SystemUser SET [Status] = 2 WHERE Id = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}