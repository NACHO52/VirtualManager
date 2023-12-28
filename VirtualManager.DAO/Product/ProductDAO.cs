using Dapper;
using System.Data;
using System.Resources;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public class ProductDAO : IProductDAO
    {
        private readonly IDbConnection _dbConnection;
        public ProductDAO(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IList<Product>> GetAll()
        {
            string sql = @"SELECT Id, [Name], [Description], Active FROM [Product]";

            return (IList<Product>)await _dbConnection.QueryAsync<Product>(sql, new { });
        }
        public async Task<Product> Get(int id)
        {
            string sql = "SELECT Id, [Name], [Description], Active FROM [Product] WHERE Id = @id";

            Product obj = await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { id });

            if(obj != null)
            {
                sql = @"SELECT Id, ResourceItemId, Quantity FROM ProductResourceItem WHERE ProductId = @id";
                obj.Resources = (IList<ProductResourceItem>)await _dbConnection.QueryAsync<ProductResourceItem>(sql, new { id });
                foreach (ProductResourceItem item in obj.Resources)
                {
                    sql = @"SELECT ResourceItem.Id, [Name], [Description], Price, MeasureType, MeasureValue FROM ResourceItem
                            WHERE Id = @id";
                    item.ResourceItem = (ResourceItem)await _dbConnection.QueryFirstOrDefaultAsync<ResourceItem>(sql, new { id = item.ResourceItemId });
                }


                sql = @"SELECT Id, TaxId, Quantity FROM [ProductTax] WHERE ProductId = @id";
                obj.Taxes = (IList<ProductTax>)await _dbConnection.QueryAsync<ProductTax>(sql, new { id });
                foreach (ProductTax item in obj.Taxes)
                {
                    sql = @"SELECT Id, [Name], [Amount], Description, Type FROM Tax WHERE Id = @id";
                    item.Tax = (Tax)await _dbConnection.QueryFirstOrDefaultAsync<Tax>(sql, new { id = item.TaxId });
                }
            }

            return obj;
        }
        public async Task Save(Product obj)
        {
            string sql = @"INSERT INTO [Product] ([Name], [Description], Active) 
                            VALUES (@name, @description, @active)";

            await _dbConnection.ExecuteAsync(sql, new { name = obj.Name, description = obj.Description, active = obj.Active });

            sql = @"SELECT TOP 1 Id FROM Product ORDER BY Id DESC";
            int lastId = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql);

            foreach (ProductResourceItem resource in obj.Resources)
            {
                sql = @"INSERT INTO [ProductResourceItem] ([ProductId], [ResourceItemId], Quantity)
                            VALUES (@productId, @resourceId, @quantity)";
                await _dbConnection.ExecuteAsync(sql, new { productId = lastId, resourceId = resource.ResourceItemId, quantity = resource.Quantity });
            }
            foreach (ProductTax tax in obj.Taxes)
            {
                sql = @"INSERT INTO [ProductTax] ([ProductId], [TaxId], Quantity)
                            VALUES (@productId, @taxId, @quantity)";
                await _dbConnection.ExecuteAsync(sql, new { productId = lastId, taxId = tax.TaxId, quantity = tax.Quantity });
            }
        }

        public async Task Update(Product obj)
        {
            string sql = @"UPDATE [Product] SET [Name] = @name, [Description] = @description, Active = @active 
                        WHERE Id = @id";

            await _dbConnection.ExecuteAsync(sql, new { id = obj.Id, name = obj.Name, description = obj.Description, active = obj.Active });

            sql = @"SELECT Id FROM ProductResourceItem WHERE ProductId = @id";
            IList<int> oldResourceItemIds = (IList<int>)await _dbConnection.QueryAsync<int>(sql, new { id = obj.Id });
            sql = @"SELECT Id FROM ProductTax WHERE ProductId = @id";
            IList<int> oldTaxIds = (IList<int>)await _dbConnection.QueryAsync<int>(sql, new { id = obj.Id });

            foreach (ProductResourceItem resource in obj.Resources)
            {
                if(resource.Id == 0)
                {
                    sql = @"INSERT INTO [ProductResourceItem] ([ProductId], [ResourceItemId], Quantity) VALUES (@productId, @resourceId, @quantity)";
                    await _dbConnection.ExecuteAsync(sql, new { productId = obj.Id, resourceId = resource.ResourceItemId, quantity = resource.Quantity });
                }
                else
                {
                    sql = @"UPDATE [ProductResourceItem] set Quantity = @quantity WHERE Id = @id";
                    await _dbConnection.ExecuteAsync(sql, new { quantity = resource.Quantity, id = resource.Id });
                    if (oldResourceItemIds.Contains(resource.Id)) oldResourceItemIds.Remove(resource.Id);
                }
            }

            sql = @"DELETE [ProductResourceItem] WHERE Id in (@ids)";
            await _dbConnection.ExecuteAsync(sql, new { ids = string.Join(",", oldResourceItemIds) });

            foreach (ProductTax tax in obj.Taxes)
            {
                if (tax.Id == 0)
                {
                    sql = @"INSERT INTO [ProductTax] ([ProductId], [TaxId], Quantity) VALUES (@productId, @taxId, @quantity)";
                    await _dbConnection.ExecuteAsync(sql, new { productId = obj.Id, taxId = tax.TaxId, quantity = tax.Quantity });
                }
                else
                {
                    sql = @"UPDATE [ProductTax] Set Quantity = (@quantity) WHERE Id = @id";
                    await _dbConnection.ExecuteAsync(sql, new { quantity = tax.Quantity, id = tax.Id });
                    if (oldTaxIds.Contains(tax.Id)) oldTaxIds.Remove(tax.Id);
                }
            }
            sql = @"DELETE [ProductTax] WHERE Id in (@ids)";
            await _dbConnection.ExecuteAsync(sql, new { ids = string.Join(",", oldTaxIds) });

        }
        public async Task Delete(int id)
        {
            string sql = @"DELETE [ProductResourceItem] WHERE ProductId = @id
                           DELETE [ProductTax] WHERE ProductId = @id
                           DELETE [Product] WHERE Id = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id });
        }

        public async Task<Product> GetLast()
        {
            string sql = "SELECT TOP 1 Id, [Name], [Description], Active FROM [Product] ORDER BY Id DESC";

            Product obj = await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { });

            if (obj != null)
            {
                sql = @"SELECT Id, ResourceItemId, Quantity FROM ProductResourceItem WHERE ProductId = @id";
                obj.Resources = (IList<ProductResourceItem>)await _dbConnection.QueryAsync<ProductResourceItem>(sql, new { obj.Id });
                foreach (ProductResourceItem item in obj.Resources)
                {
                    sql = @"SELECT ResourceItem.Id, [Name], [Description], Price, MeasureType, MeasureValue FROM ResourceItem
                            WHERE Id = @id";
                    item.ResourceItem = (ResourceItem)await _dbConnection.QueryFirstOrDefaultAsync<ResourceItem>(sql, new { id = item.ResourceItemId });
                }


                sql = @"SELECT Id, TaxId, Quantity FROM [ProductTax] WHERE ProductId = @id";
                obj.Taxes = (IList<ProductTax>)await _dbConnection.QueryAsync<ProductTax>(sql, new { obj.Id });
                foreach (ProductTax item in obj.Taxes)
                {
                    sql = @"SELECT Id, [Name], [Amount], Description, Type FROM Tax WHERE Id = @id";
                    item.Tax = (Tax)await _dbConnection.QueryFirstOrDefaultAsync<Tax>(sql, new { id = item.TaxId });
                }
            }

            return obj;
        }
    }
}