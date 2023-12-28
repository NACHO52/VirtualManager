using Dapper;
using System.Data;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public class ResourceItemDAO : IResourceItemDAO
    {
        private readonly IDbConnection _dbConnection;
        public ResourceItemDAO(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IList<ResourceItem>> GetAll()
        {
            string sql = @"SELECT Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [ResourceItem]";

            return (IList<ResourceItem>)await _dbConnection.QueryAsync<ResourceItem>(sql, new { });
        }
        public async Task<ResourceItem> Get(int id)
        {
            string sql = "SELECT Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [ResourceItem] WHERE Id = @id";

            ResourceItem obj = await _dbConnection.QueryFirstOrDefaultAsync<ResourceItem>(sql, new { id });

            if(obj != null)
            {
                sql = "SELECT Id, Price, [Date] FROM [ResourceItemPriceHistory] WHERE ResourceItemId = @id";
                obj.PriceHistory = (IList<ResourceItemPriceHistory>)await _dbConnection.QueryAsync<ResourceItemPriceHistory>(sql, new { id });
            }

            return obj;
        }
        public async Task Save(ResourceItem obj)
        {
            string sql = @"INSERT INTO [ResourceItem] ([Name], [Description], Price, MeasureType, MeasureValue) 
                            VALUES (@name, @description, @price, @measureType, @measureValue)";

            await _dbConnection.ExecuteAsync(sql, new { name = obj.Name, description = obj.Description, price = obj.Price, measureType = obj.MeasureType, measureValue = obj.MeasureValue });

            sql = @"SELECT TOP 1 Id FROM ResourceItem ORDER BY Id DESC";
            int lastId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql);

            foreach (ResourceItemPriceHistory history in obj.PriceHistory)
            {
                sql = @"INSERT INTO [ResourceItemPriceHistory] (Price, [Date], ResourceItemId)
                            VALUES (@price, @date, @resourceItemId)";

                await _dbConnection.ExecuteAsync(sql, new { price = history.Price, date = history.Date, resourceItemId = lastId});
            }
        }
        public async Task Update(ResourceItem obj)
        {
            string sql = @"UPDATE [ResourceItem] SET [Name] = @name, [Description] = @description, Price = @price, MeasureType = @measureType, MeasureValue = @measureValue 
                        WHERE Id = @id";

            await _dbConnection.ExecuteAsync(sql, new { id = obj.Id, name = obj.Name, description = obj.Description, price = obj.Price, measureType = obj.MeasureType, measureValue = obj.MeasureValue });

            foreach (ResourceItemPriceHistory history in obj.PriceHistory)
            {
                if(history.Id == 0)
                {
                    sql = @"INSERT INTO [ResourceItemPriceHistory] (Price, [Date], ResourceItemId)
                            VALUES (@price, @date, @resourceItemId)";
                    await _dbConnection.ExecuteAsync(sql, new { price = history.Price, date = history.Date, resourceItemId = obj.Id });
                }
                //else
                //{
                //    sql = @"UPDATE [ResourceItemPriceHistory] SET Price = @price, [Date] = @date WHERE Id = @id";
                //    await _dbConnection.ExecuteAsync(sql, new { price = history.Price, date = history.Date, Id = history.Id });
                //}
            }

        }
        public async Task Delete(int id)
        {
            string sql = @"DELETE [ResourceItemPriceHistory] WHERE ResourceItemId = @id
                           DELETE [ResourceItem] WHERE Id = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id });
        }

        public async Task<ResourceItem> GetLast()
        {
            string sql = "SELECT TOP 1 Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [ResourceItem] ORDER BY Id DESC";

            ResourceItem obj = await _dbConnection.QueryFirstOrDefaultAsync<ResourceItem>(sql, new { });

            if (obj != null)
            {
                sql = "SELECT Id, Price, [Date] FROM [ResourceItemPriceHistory] WHERE ResourceItemId = @id";
                obj.PriceHistory = (IList<ResourceItemPriceHistory>)await _dbConnection.QueryAsync<ResourceItemPriceHistory>(sql, new { obj.Id });
            }

            return obj;
        }

        public async Task<IList<ResourceItem>> GetExcludedByIds(string ids)
        {
            string sql = @"SELECT Id, [Name], [Description], Price, MeasureType, MeasureValue FROM [ResourceItem] WHERE Id NOT IN ("+ ids + ")";

            return (IList<ResourceItem>)await _dbConnection.QueryAsync<ResourceItem>(sql, new {  });
        }
    }
}