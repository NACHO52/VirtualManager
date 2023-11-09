using Dapper;
using System.Data;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public class ResourceDAO : IResourceDAO
    {
        private readonly IDbConnection _dbConnection;
        public ResourceDAO(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IList<Resource>> GetAll()
        {
            string sql = @"SELECT Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [Resource]";

            return (IList<Resource>)await _dbConnection.QueryAsync<Resource>(sql, new { });
        }
        public async Task<Resource> Get(int id)
        {
            string sql = "SELECT Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [Resource] WHERE Id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Resource>(sql, new { id });
        }
        public async Task Save(Resource obj)
        {
            string sql = @"INSERT INTO [Resource] ([Name], [Description], Price, MeasureType, MeasureValue) 
                            VALUES (@name, @description, @price, @measureType, @measureValue)";

            await _dbConnection.ExecuteAsync(sql, new { name = obj.Name, description = obj.Description, price = obj.Price, measureType = obj.MeasureType, measureValue = obj.MeasureValue });
        }
        public async Task Update(Resource obj)
        {
            string sql = @"UPDATE [Resource] SET [Name] = @name, [Description] = @description, Price = @price, MeasureType = @measureType, MeasureValue = @measureValue 
                        WHERE Id = @id";

            await _dbConnection.ExecuteAsync(sql, new { id = obj.Id, name = obj.Name, description = obj.Description, price = obj.Price, measureType = obj.MeasureType, measureValue = obj.MeasureValue });
        }
        public async Task Delete(int id)
        {
            string sql = "DELETE [Resource] WHERE Id = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}