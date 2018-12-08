using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Lojinha.Core.Models;
using Lojinha.Core.Entities;

namespace Lojinha.Infraestructure.Storage
{
    public class AzureStorage : IAzureStorage
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudTableClient _tableClient;
        public AzureStorage(IConfiguration config)
        {
            _account = CloudStorageAccount.Parse(config.GetSection("Azure:Storage").Value);
            _tableClient = _account.CreateCloudTableClient();
        }

        public void AddProduto(Produto produto)
        {
            var json = JsonConvert.SerializeObject(produto);
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new ProdutoEntity("13net", produto.Id.ToString()) { Produto = json };

            TableOperation operation = TableOperation.Insert(entity);
            table.ExecuteAsync(operation).Wait();
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();
            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net"));

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            var list = result.ToList();

            return list
                 .Where(x => x.Produto != null)
                .Select(x =>
                JsonConvert.DeserializeObject<Produto>(x.Produto)
            ).ToList();
        }
        public async Task<Produto> ObterProduto(string id)
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();
            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net"))
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            var produtoEntity = result.FirstOrDefault();

            return JsonConvert.DeserializeObject<Produto>(produtoEntity.Produto);
        }
    }
}
