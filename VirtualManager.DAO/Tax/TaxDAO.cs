using Dapper;
using System.Data;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public class TaxDAO : ITaxDAO
    {
        private readonly IDbConnection _dbConnection;
        public TaxDAO(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IList<Tax>> GetAll()
        {
            string sql = @"SELECT Id, [Name], [Description], Amount, Type FROM [Tax]";

            return (IList<Tax>)await _dbConnection.QueryAsync<Tax>(sql, new { });
        }
        public async Task<Tax> Get(int id)
        {
            string sql = "SELECT Id, [Name], [Description], Amount, Type FROM [Tax] WHERE Id = @id";

            Tax obj = await _dbConnection.QueryFirstOrDefaultAsync<Tax>(sql, new { id });

            if(obj != null)
            {
                sql = "SELECT Id, Amount, [Date] FROM [TaxAmountHistory] WHERE TaxId = @id";
                obj.AmountHistory = (IList<TaxAmountHistory>)await _dbConnection.QueryAsync<TaxAmountHistory>(sql, new { id });
            }

            return obj;
        }
        public async Task Save(Tax obj)
        {
            string sql = @"INSERT INTO [Tax] ([Name], [Description], Amount, [Type]) 
                            VALUES (@name, @description, @amount, @type)";

            await _dbConnection.ExecuteAsync(sql, new { name = obj.Name, description = obj.Description, amount = obj.Amount, type = obj.Type });

            sql = @"SELECT TOP 1 Id FROM Tax ORDER BY Id DESC";
            int lastId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql);

            foreach (TaxAmountHistory history in obj.AmountHistory)
            {
                sql = @"INSERT INTO [TaxAmountHistory] (Amount, [Date], TaxId)
                            VALUES (@amount, @date, @taxId)";

                await _dbConnection.ExecuteAsync(sql, new { amount = history.Amount, date = history.Date, taxId = lastId });
            }
        }
        public async Task Update(Tax obj)
        {
            string sql = @"UPDATE [Tax] SET [Name] = @name, [Description] = @description, Amount = @amount, [Type] = @type
                        WHERE Id = @id";

            await _dbConnection.ExecuteAsync(sql, new { id = obj.Id, name = obj.Name, description = obj.Description, amount = obj.Amount, type = obj.Type});

            foreach (TaxAmountHistory history in obj.AmountHistory)
            {
                if(history.Id == 0)
                {
                    sql = @"INSERT INTO [TaxAmountHistory] (Amount, [Date], TaxId)
                            VALUES (@price, @date, @taxId)";
                    await _dbConnection.ExecuteAsync(sql, new { price = history.Amount, date = history.Date, taxId = obj.Id });
                }
                //else
                //{
                //    sql = @"UPDATE [TaxAmountHistory] SET Amount = @amount, [Date] = @date WHERE Id = @id";
                //    await _dbConnection.ExecuteAsync(sql, new { price = history.Amount, date = history.Date, Id = history.Id });
                //}
            }

        }
        public async Task Delete(int id)
        {
            string sql = @"DELETE [TaxAmountHistory] WHERE TaxId = @id
                           DELETE [Tax] WHERE Id = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id });
        }

        public async Task<Tax> GetLast()
        {
            string sql = "SELECT TOP 1 Id, [Name], [Description], Amount, Type FROM [Tax] ORDER BY Id DESC";

            Tax obj = await _dbConnection.QueryFirstOrDefaultAsync<Tax>(sql, new { });

            if (obj != null)
            {
                sql = "SELECT Id, Amount, [Date] FROM [TaxAmountHistory] WHERE TaxId = @id";
                obj.AmountHistory = (IList<TaxAmountHistory>)await _dbConnection.QueryAsync<TaxAmountHistory>(sql, new { obj.Id });
            }

            return obj;
        }

        public async Task<IList<Tax>> GetExcludedByIds(string ids)
        {
            string sql = @"SELECT Id, [Name], [Description], Amount, Type FROM [Tax] WHERE Id NOT IN (" + ids + ")";

            return (IList<Tax>)await _dbConnection.QueryAsync<Tax>(sql, new { });
        }
    }
}