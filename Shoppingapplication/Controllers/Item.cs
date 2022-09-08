using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shoppingapplication.Models;

namespace Shoppingapplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Item : ControllerBase
    {
        private readonly IMongoCollection<Items> _items;
        
        public Item()
        {
            var dbHost = "host.docker.internal";
            var dbName = "item_order";
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _items = database.GetCollection<Items>("items");
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Items>>> GetItems()
        {
            return await _items.Find(Builders<Items>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{ItemId}")]

        public async Task<ActionResult<Items>> GetById(string itemId)
        {
            var filterDefinition = Builders<Items>.Filter.Eq( x => x.Id, itemId);
            return await _items.Find(filterDefinition).SingleOrDefaultAsync();

        }

        [HttpPost]

        public async Task<ActionResult> Create(Items items)
        {
            await _items.InsertOneAsync(items);
            return Ok(items);
        }




        [HttpPut]
        public async Task<ActionResult> Update(Items items)
        {
            var filterDefinition = Builders<Items>.Filter.Eq(x => x.Id, items.Id);
            await _items.ReplaceOneAsync(filterDefinition, items);
            return Ok(items);
        }

        [HttpDelete("{ItemId}")]
        public async Task<ActionResult> Delete(string itemId)
        {
            var filterDefinition = Builders<Items>.Filter.Eq(x => x.Id, itemId);
            await _items.DeleteOneAsync(filterDefinition);
            return Ok();
        }

    }
}
